using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using WebServiceStudioExpress.Properties;

namespace SpecialServices
{
	public delegate void RecentFileSelectedEventHandler(RecentFiles sender, string FileName);

	public class RecentFiles
	{
		#region Platform Invoke

		private const int SHARD_PATH = 0x2;
		[DllImport("shell32.dll")]
		private static extern void SHAddToRecentDocs(uint uFlags, string pv);

		#endregion

		private const int MAX_MENU_ITEMS = 4;
		private const int MAX_LENGTH = 24;

		private ToolStripMenuItem mMenu;
		private int mPosition;
		private int mMaxMenuItems;
		private StringCollection mFiles;
		private RecentFileSelectedEventHandler mOnClick;
		private bool mShowRecentFiles;
		
		public RecentFiles(ToolStripMenuItem Menu, RecentFileSelectedEventHandler onClick)
			: this(Menu, Menu.DropDownItems.Count, MAX_MENU_ITEMS, onClick)
		{ }

		public RecentFiles(ToolStripMenuItem Menu, int Position, RecentFileSelectedEventHandler onClick)
			: this(Menu, Position, MAX_MENU_ITEMS, onClick)
		{}

		public RecentFiles(ToolStripMenuItem Menu, int Position, int MaxMenuItems, RecentFileSelectedEventHandler onClick)
			: this(Menu, Position, MaxMenuItems, true, onClick)
		{ }
		
		public RecentFiles(ToolStripMenuItem Menu, int Position, int MaxMenuItems, bool ShowItem, RecentFileSelectedEventHandler onClick)
		{
			mFiles = Settings.Default.RecentFiles;
			if (mFiles == null)
				mFiles = new StringCollection();

			mMenu = Menu;
			mPosition = Position;
			mMaxMenuItems = MaxMenuItems;
			mOnClick = onClick;
			mShowRecentFiles = ShowItem;

			if (mShowRecentFiles)
			{
				//Visualizza l'elenco dei file recenti.
				this.DrawMenu();
			}
		}

		#region ShortenPathName

		private string ShortenPathName(string PathName)
		{
			if (PathName.Length <= MAX_LENGTH)
				return PathName;

			StringBuilder root = new StringBuilder(Path.GetPathRoot(PathName));
			if (root.Length > 3)
				root.Append(Path.DirectorySeparatorChar);

			String[] elements = PathName.Substring(root.Length).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			int filenameIndex = elements.GetLength(0) - 1;

			if (elements.GetLength(0) == 1) // pathname is just a root and filename
			{
				if (elements[0].Length > 5) // long enough to shorten
				{
					// if path is a UNC path, root may be rather long
					if (root.Length + 6 >= MAX_LENGTH)
						return root.Append(elements[0].Substring(0, 3) + "...").ToString();
					else
						return PathName.Substring(0, MAX_LENGTH - 3) + "...";
				}
			}
			else if ((root.Length + 4 + elements[filenameIndex].Length) > MAX_LENGTH) // pathname is just a root and filename
			{
				root.Append("...\\");

				int len = elements[filenameIndex].Length;
				if (len < 6)
					return root.Append(elements[filenameIndex]).ToString();

				if ((root.Length + 6) >= MAX_LENGTH)
					len = 3;
				else
					len = MAX_LENGTH - root.Length - 3;
				return root.Append(elements[filenameIndex].Substring(0, len) + "...").ToString();
			}
			else if (elements.GetLength(0) == 2)
			{
				return root.Append("...\\" + elements[1]).ToString();
			}
			else
			{
				int len = 0;
				int begin = 0;

				for (int i = 0; i < filenameIndex; i++)
				{
					if (elements[i].Length > len)
					{
						begin = i;
						len = elements[i].Length;
					}
				}

				int totalLength = PathName.Length - len + 3;
				int end = begin + 1;

				while (totalLength > MAX_LENGTH)
				{
					if (begin > 0)
						totalLength -= elements[--begin].Length - 1;

					if (totalLength <= MAX_LENGTH)
						break;

					if (end < filenameIndex)
						totalLength -= elements[++end].Length - 1;

					if (begin == 0 && end == filenameIndex)
						break;
				}

				// assemble final string

				for (int i = 0; i < begin; i++)
				{
					root.Append(elements[i]);
					root.Append('\\');
				}
				root.Append("...\\");

				for (int i = end; i < filenameIndex; i++)
				{
					root.Append(elements[i]);
					root.Append('\\');
				}
				return root.Append(elements[filenameIndex]).ToString();
			}
			return PathName;
		}

		#endregion

		private void ClearMenu()
		{
			if (mFiles.Count > 0)
			{
				//Se necessario, elimina il separatore.
				if ((mPosition + mFiles.Count) < mMenu.DropDownItems.Count)
					mMenu.DropDownItems.RemoveAt(mPosition + mFiles.Count - 1);

				//Elimina l'elenco dei file recenti.
				for (int i = (mPosition + mFiles.Count - 1); i >= mPosition; i--)
					mMenu.DropDownItems.RemoveAt(i);
			}
		}		
		
		private void DrawMenu()
		{
			if (mFiles.Count > 0)
			{
				for (int i = 0; i < mFiles.Count; i++)
				{
					if (i >= mMaxMenuItems)
					{
						mFiles.RemoveAt(i);
					} 
					else
					{
						ToolStripMenuItem item = new ToolStripMenuItem("&" + (i + 1) + " " + this.ShortenPathName(mFiles[i]), null, item_Click);
						//Nel tag dell'item è salvato il numero di ordine del file
						//recente. Questo serve perchè, dal comando di menu, sia
						//possibile risalire immediatamente al nome del file vero
						//e proprio.
						item.Tag = i;
						mMenu.DropDownItems.Insert(mPosition + i, item);
					}
				}

				//Se i file recenti non sono le ultime voci del menu, inserisce un
				//separatore dopo l'elenco.
				if ((mPosition + mFiles.Count) < mMenu.DropDownItems.Count)
					mMenu.DropDownItems.Insert(mPosition + mFiles.Count, new ToolStripSeparator());
			}
		}

		private void item_Click(object sender, EventArgs e)
		{
			string file = mFiles[int.Parse(((ToolStripMenuItem)sender).Tag.ToString())];

			//Lancia l'evento che segnala che è stato selezionato un file recente.
			if (mOnClick != null)
				mOnClick(this, file);
		}

		public void AddFile(string FileName)
		{
			if (mShowRecentFiles)
			{
				//Cancella l'elenco dei file recenti.
				this.ClearMenu();
			}

			//Controlla se il file da aggiungere è già presente nella lista: in
			//questo caso, lo sposta in cima.
			int index = this.IndexOf(FileName);
			if (index != -1)
				mFiles.RemoveAt(index);
				
			//Aggiunge (o sposta) il file all'inizio della lista.
			mFiles.Insert(0, FileName);
			SHAddToRecentDocs(SHARD_PATH, FileName);

			//Se necessario, elimina i file "meno recenti".
			if (mFiles.Count > mMaxMenuItems)
			{
				for (int i = mMaxMenuItems; i < mFiles.Count; i++)
					mFiles.RemoveAt(i);
			}

			if (mShowRecentFiles)
			{
				//Aggiorna l'elenco dei file recenti.
				this.DrawMenu();
			}

			//Salva l'elenco dei file recenti.
			Settings.Default.RecentFiles = mFiles;
		}

		public int MaxMenuItems
		{
			get { return mMaxMenuItems; }
			set 
			{
				if (value != mMaxMenuItems)
				{
					mMaxMenuItems = value;
					this.ClearMenu();
					this.DrawMenu();
				}
			}
		}

		public bool ShowRecentFiles
		{
			get { return mShowRecentFiles; }
			set 
			{
				if (value != mShowRecentFiles)
				{
					mShowRecentFiles = value;
					if (mShowRecentFiles)
					{
						this.DrawMenu();
					}
					else
					{
						this.ClearMenu();
					}
				}
			}
		}

		private int IndexOf(string value)
		{
			//Utilizza questo metodo per evitare 
			int i  = 0;
			while (i < mFiles.Count)
			{
                if (string.Compare(mFiles[i], value, true, CultureInfo.InvariantCulture) == 0)
					return i;
				i++;
			}

			return -1;
		}
	}
}
