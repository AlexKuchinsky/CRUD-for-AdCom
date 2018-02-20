using AdmissionCommittee.Domain.Entities;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AdmissionCommittee.Domain.Static
{
    public static class GeneratePDF
    {
        public static byte[] ByApplication(Application app, Enrollee enrollee)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                var boldfont = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 20, Font.BOLD);

                var appstr = new Paragraph(new Phrase("Application",boldfont));
                appstr.Alignment = 1;
                document.Add(appstr);

                var tableSpec = new PdfPTable(new float[] { 2, 18 });
                tableSpec.AddCell("Priority");
                tableSpec.AddCell("Speciality");
                for(int i = 0; i < app.Specialities.Count; i++)
                {
                    tableSpec.AddCell(app.Specialities[i].Priority.ToString());
                    tableSpec.AddCell(app.Specialities[i].Speciality.NCSQSpeciality.Name);
                }
                //string text = @"you are successfully created PDF file.";
                //Paragraph paragraph = new Paragraph();
                //paragraph.SpacingBefore = 10;
                //paragraph.SpacingAfter = 10;
                //paragraph.Alignment = Element.ALIGN_LEFT;
                //paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f, BaseColor.GREEN);
                //paragraph.Add(text);
                //document.Add(paragraph);
                document.Add(tableSpec);
                document.Close();

                var bytes = memoryStream.ToArray();
                memoryStream.Close();

                return bytes;
            }
        }
    }
}
