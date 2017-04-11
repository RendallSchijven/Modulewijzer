using Modulewijzer.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System;
using System.Collections.Generic;


namespace Modulewijzer.Converters
{
    public sealed class PdfConverter : IDisposable
    {
        private FileStream m_stream;
        private Document m_doc = new Document();
        private PdfWriter m_writer;
        private Font _bigheader = new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD);
        private Font _smallheader = new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD);
        private List<Docent> _docenten = new List<Docent>();
        private List<Competentie> _competenties = new List<Competentie>();

        public PdfConverter(string fileName)
        {
            m_stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            m_writer = PdfWriter.GetInstance(m_doc, m_stream);
            m_doc.Open();

        }
        public void AddModule(Module module)
        {
            m_doc.Add(new Paragraph("Studiewijzer", _bigheader));
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("Naam", _smallheader));
            m_doc.Add(new Paragraph(module.Naam));
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("Docenten", _smallheader));
            string s = "";
            for (int i = 0; i < _docenten.Count; i++)
            {
                if(_docenten[i].Tussenvoegsel != null)
                {
                    s += _docenten[i].Voorletters + " " + _docenten[i].Tussenvoegsel + " " + _docenten[i].Achternaam;
                }
                else
                {
                    s += _docenten[i].Voorletters + " " + _docenten[i].Achternaam;
                }

                if (i < _docenten.Count - 1) s += ", ";
            }
            m_doc.Add(new Paragraph(s));
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("Jaar van opstellen van document", _smallheader));
            m_doc.Add(new Paragraph(DateTime.Now.Year.ToString()));
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("EC's", _smallheader));
            m_doc.Add(new Paragraph(module.AantalEcs.ToString()));
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("Studiejaar en periode", _smallheader));
            m_doc.Add(new Paragraph(String.Format("Jaar {0} periode {1}", module.StudieJaar, module.Periode)));
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("Leeruitkomsten", _smallheader));
            m_doc.Add(new Paragraph(module.Leeruitkomsten));
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("Competenties", _smallheader));
            List list = new List(List.NUMERICAL);
            foreach (Competentie x in _competenties)
            {
                list.Add($"{x.Naam} ({x.Niveau})");
            }
            m_doc.Add(list);
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("Literatuur", _smallheader));
            m_doc.Add(new Paragraph(module.Literatuur));
            m_doc.Add(Chunk.NEWLINE);
            m_doc.Add(new Paragraph("Planning", _smallheader));
            m_doc.Add(new Paragraph(module.Planning));
        }
        public void AddDocent(Docent docent)
        {

            // Docent naar Pdf
            // Gebruikt nu string ipv Docent object
            _docenten.Add(docent);
        }
        public void AddCompetentie(Competentie competentie)
        {
            // Competentie naar Pdf
            // Gebruikt nu string ipv Competentie object
            _competenties.Add(competentie);
        }

        public void Dispose()
        {
            m_doc.Close();
        }
    }
}