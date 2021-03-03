using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Model;

namespace WpfApp4.PDF
{
    public class SalaryReport
    {
        private string filename;
        public SalaryReport(Dictionary<string, string> data, FirefightersViewModel members)
        {
            this.filename = String.Format("wyjazd_{0}.pdf", data["number"]);
            var titleFont = new Font("title", 12);
            titleFont.Bold = true;
            var contentFont = new Font("content", 10);

            var doc = new Document();
            var section = doc.AddSection();

            var dateParagraph = section.AddParagraph(String.Format("Opalenica, {0}", data["date"]));
            dateParagraph.Format.Alignment = ParagraphAlignment.Right;
            dateParagraph.Format.SpaceAfter = 10;

            var titleParagraph = section.AddParagraph("Oświadczenie o utraceniu dochodu.");
            titleParagraph.Format.Font = titleFont;
            titleParagraph.Format.Alignment = ParagraphAlignment.Center;
            titleParagraph.Format.SpaceAfter = 10;

            var statementParagraph = section.AddParagraph("Oświadczam, że blablabla... I balblabalbla balbla balbla balbla balblabalblabalblabalblabalblabalblabalblabalbla balbla balblabalblabalbla balblabalblabalblabalbla balbla  balblabalblabalblabalblabalbla balbla balbla  balbla balbla v balbla");
            statementParagraph.Format.Alignment = ParagraphAlignment.Justify;
            statementParagraph.Format.SpaceAfter = 10;

            var descriptionParapgraph = section.AddParagraph(String.Format("Nr ewidencyjny zdarzenia: {0}. Opis zdarzenia: {1}", data["number"], data["description"]));

            var memberListTable = section.AddTable();
            memberListTable.Borders.Width = 0.5;
            memberListTable.AddColumn().Width = Unit.FromCentimeter(4);
            memberListTable.AddColumn().Width = Unit.FromCentimeter(2);
            memberListTable.AddColumn().Width = Unit.FromCentimeter(2); 
            memberListTable.AddColumn();
            memberListTable.AddColumn();
            memberListTable.AddColumn().Width = Unit.FromCentimeter(3); 
            var header = memberListTable.AddRow();
            header.HeadingFormat = true;
            header.Cells[0].AddParagraph("Nazwisko");
            header.Cells[1].AddParagraph("Samochód");
            header.Cells[2].AddParagraph("Czas wyjazdu");
            header.Cells[3].AddParagraph("Czas powrotu");
            header.Cells[4].AddParagraph("Czas w minutach");
            header.Cells[5].AddParagraph("Podpis");
            foreach(MigraDoc.DocumentObjectModel.Tables.Cell cell in header.Cells)
            {
                cell.Format.Alignment = ParagraphAlignment.Center;
            }
            foreach (var member in members.FirefighterList)
            {
                var row = memberListTable.AddRow();
                row.Cells[0].AddParagraph(member.Name);
                row.Cells[1].AddParagraph(member.Car);
                row.Cells[2].AddParagraph(member.DispatchTime);
                row.Cells[3].AddParagraph(member.ReturnTime);
                row.Cells[4].AddParagraph(member.MinutesSpent.ToString());
            }

            var renderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(this.filename);
        }
        public void openCreated()
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(this.filename)
            {
                UseShellExecute = true
            };
            p.Start();
        }
    }

}
