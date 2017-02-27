//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.PlatformAbstractions;
//using Microsoft.AspNetCore.Hosting;
//using iTextSharp.text.pdf.draw;
//using iTextSharp.text.html;

//namespace Matriculas.Models
//{
//    /// <author>Eddy Wilmer Canaza Tito</author>
//    /// <summary>
//    /// Clase para generar el reporte de constancia de matrícula de un alumno.
//    /// </summary>
//    public class ReporteConstanciaMatricula
//    {
//        private IHostingEnvironment _env;
//        private IMatriculasRepositorys _repository;

//        /// <author>Eddy Wilmer Canaza Tito</author>
//        /// <summary>
//        /// Constructor de la clase ReporteConstanciaMatricula.
//        /// </summary>
//        /// <param name="repository">Instancia del repositorio.</param>
//        /// <param name="env">Hosting.</param>
//        public ReporteConstanciaMatricula(IMatriculasRepositorys repository, IHostingEnvironment env)
//        {
//            _repository = repository;
//            _env = env;
//        }

//        /// <author>Eddy Wilmer Canaza Tito</author>
//        /// <summary>
//        /// Método para generar el reporte de constancia de matrícula de un alumno.
//        /// </summary>
//        /// <param name="dniAlumno">Id del Alumno.</param>
//        /// <returns>Archivo PDF.</returns>
//        public FileStreamResult GenerarReporte(string dniAlumno)
//        {
//            Document document = new Document(PageSize.A4);
//            MemoryStream stream = new MemoryStream();

//            // Datos necesarios
//            var ex = _repository.GetAllAlumnos();
            
//            try
//            {
//                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
//                pdfWriter.CloseStream = false;

//                document.Open();

//                // Fonts
//                Font title = FontFactory.GetFont("Arial", 12, Font.BOLD);
//                Font bold10 = FontFactory.GetFont("Arial", 10, Font.BOLD);
//                Font body10 = FontFactory.GetFont("Arial", 10);

//                // Membrete
//                String baseUrl = _env.WebRootPath;
//                Image imagen = Image.GetInstance(baseUrl + "/img/logo_trilce_reporte.png");
//                imagen.ScaleAbsoluteWidth(150);
//                document.Add(imagen);

//                // Título del reporte
//                Paragraph titulo = new Paragraph("\nBOLETA DE MATRÍCULA", title);
//                titulo.Alignment = Element.ALIGN_CENTER;
//                document.Add(titulo);

//                var alumno = _repository.GetAlumnoByDni(dniAlumno);
//                var nextGrado = _repository.GetNextGrado(alumno.Id);
//                var nextCursos = _repository.GetCursosGradoById(nextGrado.Id);
//                // Fecha y hora
//                Chunk fechaCab = new Chunk("\nFECHA: ", bold10);
//                Chunk fecha = new Chunk(DateTime.Now.ToString(), body10);
//                Phrase pFecha = new Phrase();
//                pFecha.Add(fechaCab);
//                pFecha.Add(fecha);

//                // Datos del alumno
//                Chunk nombreCab = new Chunk("\nNOMBRE: ", bold10);
//                Chunk nombre = new Chunk(alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + ", " + alumno.Nombres, body10);
//                Phrase pNombre = new Phrase();
//                pNombre.Add(nombreCab);
//                pNombre.Add(nombre);

//                Chunk dniCab = new Chunk("\nDNI: ", bold10);
//                Chunk dni = new Chunk(alumno.Dni, body10);
//                Phrase pDni = new Phrase();
//                pDni.Add(dniCab);
//                pDni.Add(dni);

//                Chunk gradoCab = new Chunk("\nGRADO: ", bold10);
//                Chunk grado = new Chunk(nextGrado.Nombre + " de " + nextGrado.Nivel.Nombre, body10);
//                Phrase pGrado = new Phrase();
//                pGrado.Add(gradoCab);
//                pGrado.Add(grado);

//                Chunk cursosCab = new Chunk("\nCURSOS:\n ", bold10);
//                Phrase pCursos = new Phrase();
//                pCursos.Add(cursosCab);

//                Paragraph pDatos = new Paragraph();
//                pDatos.Add(pFecha);
//                pDatos.Add(pNombre);
//                pDatos.Add(pDni);
//                pDatos.Add(pGrado);
//                pDatos.Add(pCursos);

//                document.Add(pDatos);

//                PdfPTable table = new PdfPTable(2);
//                float[] anchoDeColumnas = new float[] { 20f, 10f };
//                table.SetWidths(anchoDeColumnas);
//                table.WidthPercentage = 50;

//                PdfPCell header1 = new PdfPCell(new Paragraph("NOMBRE", bold10));
//                header1.HorizontalAlignment = (Element.ALIGN_CENTER);
//                header1.BackgroundColor = WebColors.GetRgbColor("#aaaaaa");
//                table.AddCell(header1);

//                PdfPCell header2 = new PdfPCell(new Paragraph("HORAS", bold10));
//                header2.HorizontalAlignment = (Element.ALIGN_CENTER);
//                header2.BackgroundColor = WebColors.GetRgbColor("#aaaaaa");
//                table.AddCell(header2);

//                table.HeaderRows = 2;
//                foreach (Curso curso in nextCursos)
//                {
//                    PdfPCell nombreCurso = new PdfPCell(new Paragraph(curso.Nombre, body10));
//                    nombreCurso.HorizontalAlignment = (Element.ALIGN_LEFT);
//                    table.AddCell(nombreCurso);

//                    PdfPCell horasCurso = new PdfPCell(new Paragraph(curso.HorasAcademicas.ToString(), body10));
//                    horasCurso.HorizontalAlignment = (Element.ALIGN_CENTER);
//                    table.AddCell(horasCurso);
//                }
//                document.Add(table);
//            }
//            catch (DocumentException de)
//            {
//                Console.Error.WriteLine(de.Message);
//            }
//            catch (IOException ioe)
//            {
//                Console.Error.WriteLine(ioe.Message);
//            }

//            document.Close();

//            stream.Flush(); //Always catches me out
//            stream.Position = 0; //Not sure if this is required

//            return new FileStreamResult(stream, "application/pdf");
//        }
//    }
//}
