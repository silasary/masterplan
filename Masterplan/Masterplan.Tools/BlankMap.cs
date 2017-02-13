using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Masterplan.Tools
{
	internal class BlankMap
	{
		public static void Print()
		{
			PrintDialog printDialog = new PrintDialog();
			printDialog.AllowPrintToFile = false;
			if (printDialog.ShowDialog() == DialogResult.OK)
			{
				PrintDocument printDocument = new PrintDocument();
				printDocument.DocumentName = "Blank Grid";
				printDocument.PrinterSettings = printDialog.PrinterSettings;
				for (int num = 0; num != (int)printDialog.PrinterSettings.Copies; num++)
				{
					printDocument.PrintPage += new PrintPageEventHandler(BlankMap.print_blank_page);
					printDocument.Print();
				}
			}
		}

		private static void print_blank_page(object sender, PrintPageEventArgs e)
		{
			int num = e.PageSettings.PaperSize.Width / 100;
			int num2 = e.PageSettings.PaperSize.Height / 100;
			int val = e.PageBounds.Width / num;
			int val2 = e.PageBounds.Height / num2;
			int num3 = Math.Min(val, val2);
			int num4 = num * num3 + 1;
			int num5 = num2 * num3 + 1;
			Bitmap bitmap = new Bitmap(num4, num5);
			for (int num6 = 0; num6 != num4; num6++)
			{
				for (int num7 = 0; num7 != num5; num7++)
				{
					if (num6 % num3 == 0 || num7 % num3 == 0)
					{
						bitmap.SetPixel(num6, num7, Color.DarkGray);
					}
				}
			}
			int x = (e.PageBounds.Width - num4) / 2;
			int y = (e.PageBounds.Height - num5) / 2;
			Rectangle rect = new Rectangle(x, y, num4, num5);
			e.Graphics.DrawRectangle(Pens.Black, rect);
			e.Graphics.DrawImage(bitmap, rect);
		}
	}
}
