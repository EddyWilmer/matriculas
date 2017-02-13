using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Hosting;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.html;

namespace Matriculas.Models
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase para generar el reporte de lista de alumnos a través del Id de sección.
    /// </summary>
    public class ReporteLista
    {
        private IHostingEnvironment _env;
        private IMatriculasRepository _repository;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la clase ReporteLista
        /// </summary>
        /// <param name="repository">Intancia del repositorio.</param>
        /// <param name="env">Hosting.</param>
        public ReporteLista(IMatriculasRepository repository, IHostingEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para generar el reporte de lista de alumnos a través del Id de sección. 
        /// </summary>
        /// <param name="idSeccion">Id de la sección.</param>
        /// <returns>Archivo PDF.</returns>
        public FileStreamResult GenerarReporte(int idSeccion)
        {
            Document document = new Document(PageSize.A4);
            MemoryStream stream = new MemoryStream();

            // Datos necesarios
            var ex = _repository.GetAllAlumnos();
            
            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;

                document.Open();

                // Fonts
                Font title = FontFactory.GetFont("Arial", 12, Font.BOLD);
                Font bold10 = FontFactory.GetFont("Arial", 10, Font.BOLD);
                Font body10 = FontFactory.GetFont("Arial", 10);

                // Membrete
                String baseUrl = _env.WebRootPath;
                Image imagen = Image.GetInstance(baseUrl + "/img/logo_trilce_reporte.png");
                imagen.ScaleAbsoluteWidth(150);
                document.Add(imagen);

                // Título del reporte
                Paragraph titulo = new Paragraph("\nLISTA DE ALUMNOS", title);
                titulo.Alignment = Element.ALIGN_CENTER;
                document.Add(titulo);

                var lista = _repository.GetListaAlumnosByIdSeccion(idSeccion);
                var seccionLista = _repository.GetSeccionById(idSeccion);
                var gradoLista = _repository.GetGradoById(seccionLista.Grado.Id);
                // Fecha y hora
                Chunk fechaCab = new Chunk("\nFECHA: ", bold10);
                Chunk fecha = new Chunk(DateTime.Now.ToString(), body10);
                Phrase pFecha = new Phrase();
                pFecha.Add(fechaCab);
                pFecha.Add(fecha);

                Chunk gradoCab = new Chunk("\nGRADO: ", bold10);
                Chunk grado = new Chunk(gradoLista.Nombre + " de " + gradoLista.Nivel.Nombre, body10);
                Phrase pGrado = new Phrase();
                pGrado.Add(gradoCab);
                pGrado.Add(grado);

                Chunk seccionCab = new Chunk("\nSECCIÓN: ", bold10);
                Chunk seccion = new Chunk(seccionLista.Nombre + "\n\n", body10);
                Phrase pSeccion = new Phrase();
                pGrado.Add(seccionCab);
                pGrado.Add(seccion);

                Paragraph pDatos = new Paragraph();
                pDatos.Add(pFecha);
                pDatos.Add(pGrado);

                document.Add(pDatos);

                PdfPTable table = new PdfPTable(4);
                float[] anchoDeColumnas = new float[] { 5f, 40f, 20f, 20f };
                table.SetWidths(anchoDeColumnas);
                table.WidthPercentage = 100;

                PdfPCell header1 = new PdfPCell(new Paragraph("N°", bold10));
                header1.HorizontalAlignment = (Element.ALIGN_CENTER);
                header1.BackgroundColor = WebColors.GetRgbColor("#aaaaaa");
                table.AddCell(header1);

                PdfPCell header2 = new PdfPCell(new Paragraph("APELLIDOS Y NOMBRES", bold10));
                header2.HorizontalAlignment = (Element.ALIGN_CENTER);
                header2.BackgroundColor = WebColors.GetRgbColor("#aaaaaa");
                table.AddCell(header2);

                PdfPCell header3 = new PdfPCell(new Paragraph("DNI", bold10));
                header3.HorizontalAlignment = (Element.ALIGN_CENTER);
                header3.BackgroundColor = WebColors.GetRgbColor("#aaaaaa");
                table.AddCell(header3);

                PdfPCell header4 = new PdfPCell(new Paragraph("FECHA DE NACIMIENTO", bold10));
                header4.HorizontalAlignment = (Element.ALIGN_CENTER);
                header4.BackgroundColor = WebColors.GetRgbColor("#aaaaaa");
                table.AddCell(header4);

                int i = 1;
                foreach (Alumno alumno in lista)
                {
                    PdfPCell nroOrden = new PdfPCell(new Paragraph(i.ToString(), body10));
                    nroOrden.HorizontalAlignment = (Element.ALIGN_CENTER);
                    table.AddCell(nroOrden);

                    PdfPCell nombreAlumno = new PdfPCell(new Paragraph(alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + ", " + alumno.Nombres, body10));
                    nombreAlumno.HorizontalAlignment = (Element.ALIGN_LEFT);
                    table.AddCell(nombreAlumno);

                    PdfPCell dniAlumno = new PdfPCell(new Paragraph(alumno.Dni, body10));
                    dniAlumno.HorizontalAlignment = (Element.ALIGN_CENTER);
                    table.AddCell(dniAlumno);

                    PdfPCell fechaAlumno = new PdfPCell(new Paragraph(alumno.FechaNacimiento.Date.ToString("dd/MM/yyyy"), body10));
                    fechaAlumno.HorizontalAlignment = (Element.ALIGN_CENTER);
                    table.AddCell(fechaAlumno);

                    i++;
                }
                document.Add(table);
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush(); //Always catches me out
            stream.Position = 0; //Not sure if this is required
            
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}
