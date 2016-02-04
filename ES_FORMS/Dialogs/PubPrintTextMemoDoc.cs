using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ES_FORMS
{
    class PubPrintTextMemoDoc : System.Drawing.Printing.PrintDocument
    {
        RichTextBox rtb;
        Font printFont;
        int lineIndex = 0;
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            //  base.OnPrintPage(e);
            float linesPerPage = 0;
            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            int count = 0;
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            while (count < linesPerPage && lineIndex < rtb.Lines.Length)
            {
                yPos = topMargin + (count * printFont.GetHeight(e.Graphics));

                e.Graphics.DrawString(rtb.Lines[lineIndex], printFont, Brushes.Black,

                leftMargin, yPos, new StringFormat());

                count++;
                lineIndex++;
            }

            if (lineIndex < rtb.Lines.Length)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

            if (lineIndex >= rtb.Lines.Length) lineIndex = 0;

        }
        public PubPrintTextMemoDoc(RichTextBox rtb)
            : base()
        {

            printFont = new System.Drawing.Font("²Ó©úÅé_HKSCS", 10);
            this.rtb = rtb;
        }
        ~PubPrintTextMemoDoc()
        {
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
    class PubPrintMemo
    {
        private int reptdefpageindex = 0;


        private PageSettings oPageSettings;

        public System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.PrintPreviewDialog ppDialog;
        private System.Windows.Forms.PrintDialog oPrintDialog;
        private System.Windows.Forms.PageSetupDialog oPageSetup;

        public PubPrintMemo(RichTextBox rtb)
        {
            this.printDoc = new PubPrintTextMemoDoc(rtb);
            this.ppDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.oPrintDialog = new System.Windows.Forms.PrintDialog();
            this.oPageSetup = new System.Windows.Forms.PageSetupDialog();
            oPageSettings = new PageSettings();
            // 
            // oPrintDialog
            // 
            // 
            // printDoc
            // 
            //this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            this.oPrintDialog.AllowSomePages = true;
            // this.oPrintDialog.PrintToFile = true;
        }
        public void ShowPrintDialog()
        {
            //Set to defaults 

            oPrintDialog.Document = printDoc;
            if (oPrintDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Ensure the correct PrinterSettings object is used
                    oPageSettings.PrinterSettings = printDoc.PrinterSettings;
                    //Assign PageSettings object to all pages
                    printDoc.DefaultPageSettings = oPageSettings;
                    ppDialog.Document = printDoc;
                    ppDialog.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void ShowPageSetup()
        {
            oPageSettings.Margins.Top = 30;
            oPageSetup.PageSettings = oPageSettings;
            if (oPageSetup.ShowDialog() == DialogResult.OK)
            {
                oPageSettings = oPageSetup.PageSettings;
            }
        }
        public void ShowPageSetup(int MarginTop, int MarginLeft)
        {
            oPageSettings.Margins.Top = MarginTop;
            oPageSettings.Margins.Left = MarginLeft;
            oPageSetup.PageSettings = oPageSettings;
            if (oPageSetup.ShowDialog() == DialogResult.OK)
            {
                oPageSettings = oPageSetup.PageSettings;
            }
        }



    }
}
