/*-- INSERCION DE ROLES

INSERT INTO roles(Id,Nombre)
VALUES (1,'Administrador');

INSERT INTO roles(Id,Nombre)
VALUES (2,'Director');

INSERT INTO roles(Id,Nombre)
VALUES (3,'Secretaria');
*/

-- INSERCION DE COLABORADORES

--INSERT INTO colaboradores(ApellidoMaterno,ApellidoPaterno,Celular,Dni,Email,Estado,Nombres,RolId,UserName)
--VALUES ('Flores','Ramirez','952895394','72912163','jramirez','1','Julian Jesus','1','');

--INSERT INTO colaboradores(ApellidoMaterno,ApellidoPaterno,Celular,Dni,Email,Estado,Nombres,RolId,UserName)
--VALUES ('Flores','Ramirez','942893589','40153625','lramirez','1','Luis Alberto','2','');

--INSERT INTO colaboradores(ApellidoMaterno,ApellidoPaterno,Celular,Dni,Email,Estado,Nombres,RolId,UserName)
--VALUES ('Acarapi','Vera','947896693','42758652','fvera','1','Fiorela','3','');

--INSERT INTO colaboradores(ApellidoMaterno,ApellidoPaterno,Celular,Dni,Email,Estado,Nombres,RolId,UserName)
--VALUES ('Acevedo','Manriquez','922558366','41283985','mmanriquez','1','María Mireya','3','');

--INSERT INTO colaboradores(ApellidoMaterno,ApellidoPaterno,Celular,Dni,Email,Estado,Nombres,RolId,UserName)
--VALUES ('Aguilar','Dorantes','992536147','42361595','idorantes','1','Irma','3','');


/*-- INSERCION DE NIVELES

INSERT INTO niveles
VALUES ('1','Primaria');

INSERT INTO niveles
VALUES ('2','Secundaria');
*/

-- INSERCION DE GRADOS

INSERT INTO grados
VALUES ('25','1',null,'1','1ero');

INSERT INTO grados
VALUES ('20','1','1','1','2do');

INSERT INTO grados
VALUES ('20','1','2','1','3ero');

INSERT INTO grados
VALUES ('20','1','3','1','4to');

INSERT INTO grados
VALUES ('20','1','4','1','5to');

INSERT INTO grados
VALUES ('25','1','5','1','6to');

INSERT INTO grados
VALUES ('25','1','6','2','1ero');

INSERT INTO grados
VALUES ('25','1','7','2','2do');

INSERT INTO grados
VALUES ('25','1','8','2','3ero');

INSERT INTO grados
VALUES ('25','1','9','2','4to');

INSERT INTO grados
VALUES ('30','1','10','2','5to');

-- INSERCION DE SECCIONES

INSERT INTO secciones
VALUES ('1','1','A');

INSERT INTO secciones
VALUES ('1','1','B');

INSERT INTO secciones
VALUES ('1','2','A');

INSERT INTO secciones
VALUES ('1','2','B');

INSERT INTO secciones
VALUES ('1','3','A');

INSERT INTO secciones
VALUES ('1','3','B');

INSERT INTO secciones
VALUES ('1','4','A');

INSERT INTO secciones
VALUES ('1','4','B');

INSERT INTO secciones
VALUES ('1','5','A');

INSERT INTO secciones
VALUES ('1','5','B');

INSERT INTO secciones
VALUES ('1','6','A');

INSERT INTO secciones
VALUES ('1','6','B');

INSERT INTO secciones
VALUES ('1','7','A');

INSERT INTO secciones
VALUES ('1','7','B');

INSERT INTO secciones
VALUES ('1','8','A');

INSERT INTO secciones
VALUES ('1','8','B');

INSERT INTO secciones
VALUES ('1','9','A');

INSERT INTO secciones
VALUES ('1','9','B');

INSERT INTO secciones
VALUES ('1','10','A');

INSERT INTO secciones
VALUES ('1','10','B');

INSERT INTO secciones
VALUES ('1','11','A');

INSERT INTO secciones
VALUES ('1','11','B');

-- INSERCION DE CURSOS (PRIMARIA)

-- PRIMERO DE PRIMARIA
INSERT INTO cursos
VALUES ('1','1','5','Matemática');

INSERT INTO cursos
VALUES ('1','1','4','Personal Social');

INSERT INTO cursos
VALUES ('1','1','5','Ciencia y Ambiente');

INSERT INTO cursos
VALUES ('1','1','5','Comunicación Integral');

INSERT INTO cursos
VALUES ('1','1','2','Inglés');

INSERT INTO cursos
VALUES ('1','1','2','Arte');

INSERT INTO cursos
VALUES ('1','1','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','1','2','Educación Física');


-- SEGUNDO DE PRIMARIA
INSERT INTO cursos
VALUES ('1','2','5','Matemática');

INSERT INTO cursos
VALUES ('1','2','4','Personal Social');

INSERT INTO cursos
VALUES ('1','2','5','Ciencia y Ambiente');

INSERT INTO cursos
VALUES ('1','2','5','Comunicación Integral');

INSERT INTO cursos
VALUES ('1','2','2','Inglés');

INSERT INTO cursos
VALUES ('1','2','2','Arte');

INSERT INTO cursos
VALUES ('1','2','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','2','2','Educación Física');


-- TERCERO DE PRIMARIA
INSERT INTO cursos
VALUES ('1','3','5','Matemática');

INSERT INTO cursos
VALUES ('1','3','4','Personal Social');

INSERT INTO cursos
VALUES ('1','3','5','Ciencia y Ambiente');

INSERT INTO cursos
VALUES ('1','3','5','Comunicación Integral');

INSERT INTO cursos
VALUES ('1','3','2','Inglés');

INSERT INTO cursos
VALUES ('1','3','2','Arte');

INSERT INTO cursos
VALUES ('1','3','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','3','2','Educación Física');


-- CUARTO DE PRIMARIA
INSERT INTO cursos
VALUES ('1','4','5','Matemática');

INSERT INTO cursos
VALUES ('1','4','4','Personal Social');

INSERT INTO cursos
VALUES ('1','4','5','Ciencia y Ambiente');

INSERT INTO cursos
VALUES ('1','4','5','Comunicación Integral');

INSERT INTO cursos
VALUES ('1','4','2','Inglés');

INSERT INTO cursos
VALUES ('1','4','2','Arte');

INSERT INTO cursos
VALUES ('1','4','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','4','2','Educación Física');


-- QUINTO DE PRIMARIA
INSERT INTO cursos
VALUES ('1','5','5','Matemática');

INSERT INTO cursos
VALUES ('1','5','4','Personal Social');

INSERT INTO cursos
VALUES ('1','5','5','Ciencia y Ambiente');

INSERT INTO cursos
VALUES ('1','5','5','Comunicación Integral');

INSERT INTO cursos
VALUES ('1','5','2','Inglés');

INSERT INTO cursos
VALUES ('1','5','2','Arte');

INSERT INTO cursos
VALUES ('1','5','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','5','2','Educación Física');


-- SEXTO DE PRIMARIA
INSERT INTO cursos
VALUES ('1','6','5','Matemática');

INSERT INTO cursos
VALUES ('1','6','4','Personal Social');

INSERT INTO cursos
VALUES ('1','6','5','Ciencia y Ambiente');

INSERT INTO cursos
VALUES ('1','6','5','Comunicación Integral');

INSERT INTO cursos
VALUES ('1','6','2','Inglés');

INSERT INTO cursos
VALUES ('1','6','2','Arte');

INSERT INTO cursos
VALUES ('1','6','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','6','2','Educación Física');

-- INSERCION DE CURSOS (SECUNDARIA)

-- PRIMERO DE SECUNDARIA
INSERT INTO cursos
VALUES ('1','7','6','Matemática');

INSERT INTO cursos
VALUES ('1','7','3','Formación Ciudadana');

INSERT INTO cursos
VALUES ('1','7','6','CTA');

INSERT INTO cursos
VALUES ('1','7','6','Comunicación');

INSERT INTO cursos
VALUES ('1','7','4','Historia y Geografía');

INSERT INTO cursos
VALUES ('1','7','2','Inglés');

INSERT INTO cursos
VALUES ('1','7','2','Arte');

INSERT INTO cursos
VALUES ('1','7','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','7','3','Educación Física');


-- SEGUNDO DE SECUNDARIA
INSERT INTO cursos
VALUES ('1','8','6','Matemática');

INSERT INTO cursos
VALUES ('1','8','3','Formación Ciudadana');

INSERT INTO cursos
VALUES ('1','8','6','CTA');

INSERT INTO cursos
VALUES ('1','8','6','Comunicación');

INSERT INTO cursos
VALUES ('1','8','4','Historia y Geografía');

INSERT INTO cursos
VALUES ('1','8','2','Inglés');

INSERT INTO cursos
VALUES ('1','8','2','Arte');

INSERT INTO cursos
VALUES ('1','8','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','8','3','Educación Física');


-- TERCERO DE SECUNDARIA
INSERT INTO cursos
VALUES ('1','9','6','Matemática');

INSERT INTO cursos
VALUES ('1','9','3','Formación Ciudadana');

INSERT INTO cursos
VALUES ('1','9','6','CTA');

INSERT INTO cursos
VALUES ('1','9','6','Comunicación');

INSERT INTO cursos
VALUES ('1','9','4','Historia y Geografía');

INSERT INTO cursos
VALUES ('1','9','2','Inglés');

INSERT INTO cursos
VALUES ('1','9','2','Arte');

INSERT INTO cursos
VALUES ('1','9','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','9','3','Educación Física');


-- CUARTO DE SECUNDARIA
INSERT INTO cursos
VALUES ('1','10','6','Matemática');

INSERT INTO cursos
VALUES ('1','10','3','Formación Ciudadana');

INSERT INTO cursos
VALUES ('1','10','5','CTA');

INSERT INTO cursos
VALUES ('1','10','5','Comunicación');

INSERT INTO cursos
VALUES ('1','10','4','Historia y Geografía');

INSERT INTO cursos
VALUES ('1','10','2','Persona, Familia y RRHH');

INSERT INTO cursos
VALUES ('1','10','2','Inglés');

INSERT INTO cursos
VALUES ('1','10','2','Arte');

INSERT INTO cursos
VALUES ('1','10','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','10','3','Educación Física');


-- QUINTO DE SECUNDARIA
INSERT INTO cursos
VALUES ('1','11','6','Matemática');

INSERT INTO cursos
VALUES ('1','11','3','Formación Ciudadana');

INSERT INTO cursos
VALUES ('1','11','5','CTA');

INSERT INTO cursos
VALUES ('1','11','5','Comunicación');

INSERT INTO cursos
VALUES ('1','11','4','Historia y Geografía');

INSERT INTO cursos
VALUES ('1','11','2','Persona, Familia y RRHH');

INSERT INTO cursos
VALUES ('1','11','2','Inglés');

INSERT INTO cursos
VALUES ('1','11','2','Arte');

INSERT INTO cursos
VALUES ('1','11','2','Educación Religiosa');

INSERT INTO cursos
VALUES ('1','11','3','Educación Física');

-- INSERCION DE PROFESORES

INSERT INTO profesores
VALUES ('Alvarez','Martinez','995825365','40256159','vmartinez','1','Verónica','F');

INSERT INTO profesores
VALUES ('Barreto','López','975836152','42159136','jlopez','1','Juan Ramón','M');

INSERT INTO profesores
VALUES ('Bolaños','Sánchez','942839959','41259436','csanchez','1','César','M');

INSERT INTO profesores
VALUES ('Cano','Figueroa','923614259','73259536','efigueroa','1','Erick','M');

INSERT INTO profesores
VALUES ('Durand','Ojeda','991535256','43251696','lojeda','1','Lourdes','F');

INSERT INTO profesores
VALUES ('Fernández','Castillo','972591586','40251663','rcastillo','1','Raúl','M');

INSERT INTO profesores
VALUES ('García','González','943625136','42050165','mgonzales','1','Miriam Aidé','F');

INSERT INTO profesores
VALUES ('Gallardo','Vara','915862342','42051536','rvara','1','Ricardo','M');

-- INSERCION DE PROFESOR-CURSOS

INSERT INTO ProfesoresCursos
VALUES ('1','1');

INSERT INTO ProfesoresCursos
VALUES ('1','9');

INSERT INTO ProfesoresCursos
VALUES ('1','17');

INSERT INTO ProfesoresCursos
VALUES ('2','2');

INSERT INTO ProfesoresCursos
VALUES ('2','10');

INSERT INTO ProfesoresCursos
VALUES ('2','18');

INSERT INTO ProfesoresCursos
VALUES ('3','3');

INSERT INTO ProfesoresCursos
VALUES ('3','11');

INSERT INTO ProfesoresCursos
VALUES ('3','19');

INSERT INTO ProfesoresCursos
VALUES ('4','4');

INSERT INTO ProfesoresCursos
VALUES ('4','12');

INSERT INTO ProfesoresCursos
VALUES ('4','20');

INSERT INTO ProfesoresCursos
VALUES ('5','5');

INSERT INTO ProfesoresCursos
VALUES ('5','13');

INSERT INTO ProfesoresCursos
VALUES ('5','21');

INSERT INTO ProfesoresCursos
VALUES ('6','6');

INSERT INTO ProfesoresCursos
VALUES ('6','14');

INSERT INTO ProfesoresCursos
VALUES ('6','22');

INSERT INTO ProfesoresCursos
VALUES ('7','7');

INSERT INTO ProfesoresCursos
VALUES ('7','15');

INSERT INTO ProfesoresCursos
VALUES ('7','23');

INSERT INTO ProfesoresCursos
VALUES ('8','8');

INSERT INTO ProfesoresCursos
VALUES ('8','16');

INSERT INTO ProfesoresCursos
VALUES ('8','24');

-- INSERCION DE APODERADOS

INSERT INTO apoderados
VALUES ('Erazo','Bernal','72912182','1','S','Carlos Luis','M');

INSERT INTO apoderados
VALUES ('Cortés','García','45182935','1','C','Aristeo','M');

INSERT INTO apoderados
VALUES ('Dávila','Montero','29183952','1','C','Claudia Valeria','F');

INSERT INTO apoderados
VALUES ('Escobar','Beltrán','54020395','1','S','Francisco Enrique','M');

INSERT INTO apoderados
VALUES ('Fernández','Castillo','43159502','1','C','Raúl','M');

INSERT INTO apoderados
VALUES ('Gallegos','Verdías','84265169','1','S','Rosa Elena','F');

INSERT INTO apoderados
VALUES ('García','García','95284595','1','D','Iván','M');

INSERT INTO apoderados
VALUES ('Ferrer','Chávez','48621539','1','D','María Manuela','F');

INSERT INTO apoderados
VALUES ('Flores','Salinas','36158525','1','S','Lorenzo Valentín','M');

INSERT INTO apoderados
VALUES ('Galindo','Andrade','82159536','1','S','Carlos','M');

INSERT INTO apoderados
VALUES ('Chavez','Quiroz','33150285','1','C','Carlos','M');

INSERT INTO apoderados
VALUES ('Alva','Campos','55120336','1','C','Veronica','F');

INSERT INTO apoderados
VALUES ('Benavides','Espejo','40120015','1','C','Claudia','F');

INSERT INTO apoderados
VALUES ('Cortez','Lozano','40420200','1','C','Fernando','M');

INSERT INTO apoderados
VALUES ('Ferro','Salas','42223036','1','C','Paola','F');

INSERT INTO apoderados
VALUES ('Huamani','Flores','41122002','1','C','Elena','F');

INSERT INTO apoderados
VALUES ('Perez','Valverde','41256985','1','C','Pedro','M');

INSERT INTO apoderados
VALUES ('Yana','Navarro','85153600','1','D','Manuel','M');

INSERT INTO apoderados
VALUES ('Carpio','Ortiz','62155820','1','C','Lorenzo','M');

INSERT INTO apoderados
VALUES ('Flores','Vilchez','72915800','1','C','Pablo','M');


-- INSERCION DE ALUMNOS

INSERT INTO alumnos
VALUES ('Erazo','Bernal','1','Calle Las Donas 123','81256336','1','2002/06/21','Maria Soledad','F');

INSERT INTO alumnos
VALUES ('Flores','García','2','Calle Los pajaros 123','72815814','1','2002/04/12','Jocelyn Estefany','F');

INSERT INTO alumnos
VALUES ('Rodriguez','Montero','3','Av. Siempre viva 251','72545563','1','2003/08/01','Juan Jose','M');

INSERT INTO alumnos
VALUES ('Escobar','Beltrán','4','Av. Carmen 250','72021563','1','2002/11/25','Miguel Andre','M');

INSERT INTO alumnos
VALUES ('Nieto','Castillo','5','Urb. Las buganvillas E-15','72000236','1','2001/02/12','Fiorela Milagros','M');

INSERT INTO alumnos
VALUES ('Gallegos','Verdías','6','Urb. Las buganvillas A-20','72953605','1','2002/08/27','Benito Esteban','M');

INSERT INTO alumnos
VALUES ('Perez','García','7','Las buganvillas C-25','52020214','1','2003/08/02','Raul','M');

INSERT INTO alumnos
VALUES ('Tong','Chávez','8','Av. Siempre viva 100','02362514','1','2004/01/15','Bruno','M');

INSERT INTO alumnos
VALUES ('Flores','Salinas','9','Coop. La cantuta H-11','72021525','1','2004/02/21','Yenny Jimena','F');

INSERT INTO alumnos
VALUES ('Galindo','Andrade','10','Coop. La cantuta E-1','72819536','1','2004/06/28','Eddy','M');

INSERT INTO alumnos
VALUES ('Chavez','Quiroz','11','Av. Pedregal 241','25482136','1','2001/06/10','Vidal','M');

INSERT INTO alumnos
VALUES ('Alva','Campos','12','Urb. Los Sauces B-15','36153248','1','2003/05/02','Victor','M');

INSERT INTO alumnos
VALUES ('Benavides','Espejo','13','Av. Ejercito 552','36195851','1','2002/02/05','Javier','M');

INSERT INTO alumnos
VALUES ('Cortez','Lozano','14','Av. Carmen 150','26150203','1','2002/09/10','Maribel Corina','F');

INSERT INTO alumnos
VALUES ('Ferro','Salas','15','Coop. Las Lomas F-21','15026202','1','2001/02/25','Olga','F');

INSERT INTO alumnos
VALUES ('Huamani','Flores','16','Av. Las Torres B-11','51263950','1','2002/06/10','Lourdes','F');

INSERT INTO alumnos
VALUES ('Robles','Valverde','17','Las Casuarinas D-20','95250300','1','2002/02/01','Rosa Liliana','F');

INSERT INTO alumnos
VALUES ('Maravi','Navarro','18','Av. Siempre viva 300','62051495','1','2002/01/05','Magda Janeth','F');

INSERT INTO alumnos
VALUES ('Orrillo','Ortiz','19','Coop. La cantuta D-02','62362004','1','2002/10/10','Josué Victor','M');

INSERT INTO alumnos
VALUES ('Prada','Vilchez','20','Coop. La Terna B-25','50252111','1','2002/05/10','Sonia','F');


-- INSERCION DE ANIOS ACADEMICOS

INSERT INTO aniosacademicos
VALUES ('1','2016/12/14','2016/03/01','2016');

INSERT INTO aniosacademicos
VALUES ('1','2017/12/08','2017/03/01','2017');

-- INSERCION DE MATRICULAS

INSERT INTO matriculas
VALUES ('1','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('2','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('3','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('4','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('5','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('6','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('7','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('8','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('9','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('10','1','2016/02/09','1','1');

INSERT INTO matriculas
VALUES ('11','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('12','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('13','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('14','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('15','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('16','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('17','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('18','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('19','1','2016/02/09','1','2');

INSERT INTO matriculas
VALUES ('20','1','2016/02/09','1','2');


-- NOTAS DE ALUMNOS

INSERT INTO notas
VALUES ('1','1','10');

INSERT INTO notas
VALUES ('1','2','15');

INSERT INTO notas
VALUES ('1','3','16');

INSERT INTO notas
VALUES ('1','4','09');

INSERT INTO notas
VALUES ('1','5','12');

INSERT INTO notas
VALUES ('1','6','10');

INSERT INTO notas
VALUES ('1','7','15');

INSERT INTO notas
VALUES ('1','8','12');

INSERT INTO notas
VALUES ('2','1','16');

INSERT INTO notas
VALUES ('2','2','17');

INSERT INTO notas
VALUES ('2','3','18');

INSERT INTO notas
VALUES ('2','4','17');

INSERT INTO notas
VALUES ('2','5','18');

INSERT INTO notas
VALUES ('2','6','17');

INSERT INTO notas
VALUES ('2','7','18');

INSERT INTO notas
VALUES ('2','8','16');

INSERT INTO notas
VALUES ('3','1','16');

INSERT INTO notas
VALUES ('3','2','17');

INSERT INTO notas
VALUES ('3','3','17');

INSERT INTO notas
VALUES ('3','4','18');

INSERT INTO notas
VALUES ('3','5','18');

INSERT INTO notas
VALUES ('3','6','18');

INSERT INTO notas
VALUES ('3','7','18');

INSERT INTO notas
VALUES ('3','8','19');

INSERT INTO notas
VALUES ('4','1','08');

INSERT INTO notas
VALUES ('4','2','06');

INSERT INTO notas
VALUES ('4','3','08');

INSERT INTO notas
VALUES ('4','4','12');

INSERT INTO notas
VALUES ('4','5','12');

INSERT INTO notas
VALUES ('4','6','11');

INSERT INTO notas
VALUES ('4','7','11');

INSERT INTO notas
VALUES ('4','8','11');

INSERT INTO notas
VALUES ('5','1','15');

INSERT INTO notas
VALUES ('5','2','15');

INSERT INTO notas
VALUES ('5','3','18');

INSERT INTO notas
VALUES ('5','4','18');

INSERT INTO notas
VALUES ('5','5','19');

INSERT INTO notas
VALUES ('5','6','15');

INSERT INTO notas
VALUES ('5','7','16');

INSERT INTO notas
VALUES ('5','8','17');

INSERT INTO notas
VALUES ('6','1','14');

INSERT INTO notas
VALUES ('6','2','14');

INSERT INTO notas
VALUES ('6','3','14');

INSERT INTO notas
VALUES ('6','4','16');

INSERT INTO notas
VALUES ('6','5','13');

INSERT INTO notas
VALUES ('6','6','12');

INSERT INTO notas
VALUES ('6','7','12');

INSERT INTO notas
VALUES ('6','8','13');

INSERT INTO notas
VALUES ('7','1','16');

INSERT INTO notas
VALUES ('7','2','16');

INSERT INTO notas
VALUES ('7','3','16');

INSERT INTO notas
VALUES ('7','4','18');

INSERT INTO notas
VALUES ('7','5','12');

INSERT INTO notas
VALUES ('7','6','13');

INSERT INTO notas
VALUES ('7','7','14');

INSERT INTO notas
VALUES ('7','8','14');

INSERT INTO notas
VALUES ('8','1','19');

INSERT INTO notas
VALUES ('8','2','18');

INSERT INTO notas
VALUES ('8','3','19');

INSERT INTO notas
VALUES ('8','4','18');

INSERT INTO notas
VALUES ('8','5','19');

INSERT INTO notas
VALUES ('8','6','18');

INSERT INTO notas
VALUES ('8','7','19');

INSERT INTO notas
VALUES ('8','8','18');

INSERT INTO notas
VALUES ('9','1','16');

INSERT INTO notas
VALUES ('9','2','16');

INSERT INTO notas
VALUES ('9','3','14');

INSERT INTO notas
VALUES ('9','4','14');

INSERT INTO notas
VALUES ('9','5','15');

INSERT INTO notas
VALUES ('9','6','14');

INSERT INTO notas
VALUES ('9','7','15');

INSERT INTO notas
VALUES ('9','8','15');

INSERT INTO notas
VALUES ('10','1','15');

INSERT INTO notas
VALUES ('10','2','16');

INSERT INTO notas
VALUES ('10','3','18');

INSERT INTO notas
VALUES ('10','4','19');

INSERT INTO notas
VALUES ('10','5','17');

INSERT INTO notas
VALUES ('10','6','16');

INSERT INTO notas
VALUES ('10','7','14');

INSERT INTO notas
VALUES ('10','8','15');

INSERT INTO notas
VALUES ('11','1','15');

INSERT INTO notas
VALUES ('11','2','16');

INSERT INTO notas
VALUES ('11','3','18');

INSERT INTO notas
VALUES ('11','4','19');

INSERT INTO notas
VALUES ('11','5','16');

INSERT INTO notas
VALUES ('11','6','17');

INSERT INTO notas
VALUES ('11','7','14');

INSERT INTO notas
VALUES ('11','8','15');

INSERT INTO notas
VALUES ('12','1','12');

INSERT INTO notas
VALUES ('12','2','13');

INSERT INTO notas
VALUES ('12','3','12');

INSERT INTO notas
VALUES ('12','4','13');

INSERT INTO notas
VALUES ('12','5','14');

INSERT INTO notas
VALUES ('12','6','15');

INSERT INTO notas
VALUES ('12','7','16');

INSERT INTO notas
VALUES ('12','8','13');

INSERT INTO notas
VALUES ('13','1','15');

INSERT INTO notas
VALUES ('13','2','15');

INSERT INTO notas
VALUES ('13','3','16');

INSERT INTO notas
VALUES ('13','4','18');

INSERT INTO notas
VALUES ('13','5','18');

INSERT INTO notas
VALUES ('13','6','19');

INSERT INTO notas
VALUES ('13','7','16');

INSERT INTO notas
VALUES ('13','8','17');

INSERT INTO notas
VALUES ('14','1','15');

INSERT INTO notas
VALUES ('14','2','16');

INSERT INTO notas
VALUES ('14','3','15');

INSERT INTO notas
VALUES ('14','4','16');

INSERT INTO notas
VALUES ('14','5','15');

INSERT INTO notas
VALUES ('14','6','16');

INSERT INTO notas
VALUES ('14','7','15');

INSERT INTO notas
VALUES ('14','8','16');

INSERT INTO notas
VALUES ('15','1','15');

INSERT INTO notas
VALUES ('15','2','14');

INSERT INTO notas
VALUES ('15','3','13');

INSERT INTO notas
VALUES ('15','4','14');

INSERT INTO notas
VALUES ('15','5','13');

INSERT INTO notas
VALUES ('15','6','12');

INSERT INTO notas
VALUES ('15','7','13');

INSERT INTO notas
VALUES ('15','8','12');

INSERT INTO notas
VALUES ('16','1','18');

INSERT INTO notas
VALUES ('16','2','17');

INSERT INTO notas
VALUES ('16','3','18');

INSERT INTO notas
VALUES ('16','4','17');

INSERT INTO notas
VALUES ('16','5','18');

INSERT INTO notas
VALUES ('16','6','17');

INSERT INTO notas
VALUES ('16','7','18');

INSERT INTO notas
VALUES ('16','8','17');

INSERT INTO notas
VALUES ('17','1','19');

INSERT INTO notas
VALUES ('17','2','18');

INSERT INTO notas
VALUES ('17','3','17');

INSERT INTO notas
VALUES ('17','4','18');

INSERT INTO notas
VALUES ('17','5','17');

INSERT INTO notas
VALUES ('17','6','19');

INSERT INTO notas
VALUES ('17','7','18');

INSERT INTO notas
VALUES ('17','8','19');

INSERT INTO notas
VALUES ('18','1','15');

INSERT INTO notas
VALUES ('18','2','14');

INSERT INTO notas
VALUES ('18','3','13');

INSERT INTO notas
VALUES ('18','4','12');

INSERT INTO notas
VALUES ('18','5','15');

INSERT INTO notas
VALUES ('18','6','13');

INSERT INTO notas
VALUES ('18','7','12');

INSERT INTO notas
VALUES ('18','8','13');

INSERT INTO notas
VALUES ('19','1','19');

INSERT INTO notas
VALUES ('19','2','15');

INSERT INTO notas
VALUES ('19','3','16');

INSERT INTO notas
VALUES ('19','4','18');

INSERT INTO notas
VALUES ('19','5','17');

INSERT INTO notas
VALUES ('19','6','19');

INSERT INTO notas
VALUES ('19','7','16');

INSERT INTO notas
VALUES ('19','8','16');

INSERT INTO notas
VALUES ('20','1','15');

INSERT INTO notas
VALUES ('20','2','16');

INSERT INTO notas
VALUES ('20','3','12');

INSERT INTO notas
VALUES ('20','4','12');

INSERT INTO notas
VALUES ('20','5','13');

INSERT INTO notas
VALUES ('20','6','14');

INSERT INTO notas
VALUES ('20','7','15');

INSERT INTO notas
VALUES ('20','8','16');

-- INSERCION DE DEUDAS

INSERT INTO deudas
VALUES ('1','3','0');

INSERT INTO deudas
VALUES ('1','4','0');

INSERT INTO deudas
VALUES ('1','5','0');

INSERT INTO deudas
VALUES ('1','6','0');

INSERT INTO deudas
VALUES ('1','7','0');

INSERT INTO deudas
VALUES ('1','8','0');

INSERT INTO deudas
VALUES ('1','9','0');

INSERT INTO deudas
VALUES ('1','10','0');

INSERT INTO deudas
VALUES ('1','11','0');

INSERT INTO deudas
VALUES ('1','12','0');

INSERT INTO deudas
VALUES ('2','3','0');

INSERT INTO deudas
VALUES ('2','4','0');

INSERT INTO deudas
VALUES ('2','5','0');

INSERT INTO deudas
VALUES ('2','6','0');

INSERT INTO deudas
VALUES ('2','7','0');

INSERT INTO deudas
VALUES ('2','8','0');

INSERT INTO deudas
VALUES ('2','9','0');

INSERT INTO deudas
VALUES ('2','10','0');

INSERT INTO deudas
VALUES ('2','11','0');

INSERT INTO deudas
VALUES ('2','12','0');

INSERT INTO deudas
VALUES ('3','3','0');

INSERT INTO deudas
VALUES ('3','4','0');

INSERT INTO deudas
VALUES ('3','5','0');

INSERT INTO deudas
VALUES ('3','6','0');

INSERT INTO deudas
VALUES ('3','7','0');

INSERT INTO deudas
VALUES ('3','8','0');

INSERT INTO deudas
VALUES ('3','9','0');

INSERT INTO deudas
VALUES ('3','10','0');

INSERT INTO deudas
VALUES ('3','11','0');

INSERT INTO deudas
VALUES ('3','12','0');

INSERT INTO deudas
VALUES ('4','3','0');

INSERT INTO deudas
VALUES ('4','4','0');

INSERT INTO deudas
VALUES ('4','5','0');

INSERT INTO deudas
VALUES ('4','6','0');

INSERT INTO deudas
VALUES ('4','7','0');

INSERT INTO deudas
VALUES ('4','8','0');

INSERT INTO deudas
VALUES ('4','9','0');

INSERT INTO deudas
VALUES ('4','10','0');

INSERT INTO deudas
VALUES ('4','11','0');

INSERT INTO deudas
VALUES ('4','12','250');

INSERT INTO deudas
VALUES ('5','3','0');

INSERT INTO deudas
VALUES ('5','4','0');

INSERT INTO deudas
VALUES ('5','5','0');

INSERT INTO deudas
VALUES ('5','6','0');

INSERT INTO deudas
VALUES ('5','7','0');

INSERT INTO deudas
VALUES ('5','8','0');

INSERT INTO deudas
VALUES ('5','9','0');

INSERT INTO deudas
VALUES ('5','10','0');

INSERT INTO deudas
VALUES ('5','11','250');

INSERT INTO deudas
VALUES ('5','12','250');

INSERT INTO deudas
VALUES ('6','3','0');

INSERT INTO deudas
VALUES ('6','4','0');

INSERT INTO deudas
VALUES ('6','5','0');

INSERT INTO deudas
VALUES ('6','6','0');

INSERT INTO deudas
VALUES ('6','7','0');

INSERT INTO deudas
VALUES ('6','8','0');

INSERT INTO deudas
VALUES ('6','9','0');

INSERT INTO deudas
VALUES ('6','10','250');

INSERT INTO deudas
VALUES ('6','11','250');

INSERT INTO deudas
VALUES ('6','12','250');

INSERT INTO deudas
VALUES ('7','3','0');

INSERT INTO deudas
VALUES ('7','4','0');

INSERT INTO deudas
VALUES ('7','5','0');

INSERT INTO deudas
VALUES ('7','6','0');

INSERT INTO deudas
VALUES ('7','7','0');

INSERT INTO deudas
VALUES ('7','8','0');

INSERT INTO deudas
VALUES ('7','9','0');

INSERT INTO deudas
VALUES ('7','10','0');

INSERT INTO deudas
VALUES ('7','11','0');

INSERT INTO deudas
VALUES ('7','12','250');

INSERT INTO deudas
VALUES ('8','3','0');

INSERT INTO deudas
VALUES ('8','4','0');

INSERT INTO deudas
VALUES ('8','5','0');

INSERT INTO deudas
VALUES ('8','6','0');

INSERT INTO deudas
VALUES ('8','7','0');

INSERT INTO deudas
VALUES ('8','8','0');

INSERT INTO deudas
VALUES ('8','9','0');

INSERT INTO deudas
VALUES ('8','10','0');

INSERT INTO deudas
VALUES ('8','11','250');

INSERT INTO deudas
VALUES ('8','12','250');

INSERT INTO deudas
VALUES ('9','3','0');

INSERT INTO deudas
VALUES ('9','4','0');

INSERT INTO deudas
VALUES ('9','5','0');

INSERT INTO deudas
VALUES ('9','6','0');

INSERT INTO deudas
VALUES ('9','7','0');

INSERT INTO deudas
VALUES ('9','8','0');

INSERT INTO deudas
VALUES ('9','9','0');

INSERT INTO deudas
VALUES ('9','10','0');

INSERT INTO deudas
VALUES ('9','11','0');

INSERT INTO deudas
VALUES ('9','12','0');

INSERT INTO deudas
VALUES ('10','3','0');

INSERT INTO deudas
VALUES ('10','4','0');

INSERT INTO deudas
VALUES ('10','5','0');

INSERT INTO deudas
VALUES ('10','6','0');

INSERT INTO deudas
VALUES ('10','7','0');

INSERT INTO deudas
VALUES ('10','8','0');

INSERT INTO deudas
VALUES ('10','9','0');

INSERT INTO deudas
VALUES ('10','10','0');

INSERT INTO deudas
VALUES ('10','11','0');

INSERT INTO deudas
VALUES ('10','12','0');

INSERT INTO deudas
VALUES ('11','3','0');

INSERT INTO deudas
VALUES ('11','4','0');

INSERT INTO deudas
VALUES ('11','5','0');

INSERT INTO deudas
VALUES ('11','6','0');

INSERT INTO deudas
VALUES ('11','7','0');

INSERT INTO deudas
VALUES ('11','8','0');

INSERT INTO deudas
VALUES ('11','9','0');

INSERT INTO deudas
VALUES ('11','10','0');

INSERT INTO deudas
VALUES ('11','11','0');

INSERT INTO deudas
VALUES ('11','12','0');

INSERT INTO deudas
VALUES ('12','3','0');

INSERT INTO deudas
VALUES ('12','4','0');

INSERT INTO deudas
VALUES ('12','5','0');

INSERT INTO deudas
VALUES ('12','6','0');

INSERT INTO deudas
VALUES ('12','7','0');

INSERT INTO deudas
VALUES ('12','8','0');

INSERT INTO deudas
VALUES ('12','9','0');

INSERT INTO deudas
VALUES ('12','10','0');

INSERT INTO deudas
VALUES ('12','11','0');

INSERT INTO deudas
VALUES ('12','12','0');

INSERT INTO deudas
VALUES ('13','3','0');

INSERT INTO deudas
VALUES ('13','4','0');

INSERT INTO deudas
VALUES ('13','5','0');

INSERT INTO deudas
VALUES ('13','6','0');

INSERT INTO deudas
VALUES ('13','7','0');

INSERT INTO deudas
VALUES ('13','8','0');

INSERT INTO deudas
VALUES ('13','9','0');

INSERT INTO deudas
VALUES ('13','10','0');

INSERT INTO deudas
VALUES ('13','11','0');

INSERT INTO deudas
VALUES ('13','12','0');

INSERT INTO deudas
VALUES ('14','3','0');

INSERT INTO deudas
VALUES ('14','4','0');

INSERT INTO deudas
VALUES ('14','5','0');

INSERT INTO deudas
VALUES ('14','6','0');

INSERT INTO deudas
VALUES ('14','7','0');

INSERT INTO deudas
VALUES ('14','8','0');

INSERT INTO deudas
VALUES ('14','9','0');

INSERT INTO deudas
VALUES ('14','10','0');

INSERT INTO deudas
VALUES ('14','11','0');

INSERT INTO deudas
VALUES ('14','12','0');

INSERT INTO deudas
VALUES ('15','3','0');

INSERT INTO deudas
VALUES ('15','4','0');

INSERT INTO deudas
VALUES ('15','5','0');

INSERT INTO deudas
VALUES ('15','6','0');

INSERT INTO deudas
VALUES ('15','7','0');

INSERT INTO deudas
VALUES ('15','8','0');

INSERT INTO deudas
VALUES ('15','9','0');

INSERT INTO deudas
VALUES ('15','10','0');

INSERT INTO deudas
VALUES ('15','11','0');

INSERT INTO deudas
VALUES ('15','12','0');

INSERT INTO deudas
VALUES ('16','3','0');

INSERT INTO deudas
VALUES ('16','4','0');

INSERT INTO deudas
VALUES ('16','5','0');

INSERT INTO deudas
VALUES ('16','6','0');

INSERT INTO deudas
VALUES ('16','7','0');

INSERT INTO deudas
VALUES ('16','8','0');

INSERT INTO deudas
VALUES ('16','9','0');

INSERT INTO deudas
VALUES ('16','10','250');

INSERT INTO deudas
VALUES ('16','11','250');

INSERT INTO deudas
VALUES ('16','12','250');

INSERT INTO deudas
VALUES ('17','3','0');

INSERT INTO deudas
VALUES ('17','4','0');

INSERT INTO deudas
VALUES ('17','5','0');

INSERT INTO deudas
VALUES ('17','6','0');

INSERT INTO deudas
VALUES ('17','7','0');

INSERT INTO deudas
VALUES ('17','8','0');

INSERT INTO deudas
VALUES ('17','9','0');

INSERT INTO deudas
VALUES ('17','10','0');

INSERT INTO deudas
VALUES ('17','11','0');

INSERT INTO deudas
VALUES ('17','12','250');

INSERT INTO deudas
VALUES ('18','3','0');

INSERT INTO deudas
VALUES ('18','4','0');

INSERT INTO deudas
VALUES ('18','5','0');

INSERT INTO deudas
VALUES ('18','6','0');

INSERT INTO deudas
VALUES ('18','7','0');

INSERT INTO deudas
VALUES ('18','8','0');

INSERT INTO deudas
VALUES ('18','9','0');

INSERT INTO deudas
VALUES ('18','10','0');

INSERT INTO deudas
VALUES ('18','11','0');

INSERT INTO deudas
VALUES ('18','12','250');

INSERT INTO deudas
VALUES ('19','3','0');

INSERT INTO deudas
VALUES ('19','4','0');

INSERT INTO deudas
VALUES ('19','5','0');

INSERT INTO deudas
VALUES ('19','6','0');

INSERT INTO deudas
VALUES ('19','7','0');

INSERT INTO deudas
VALUES ('19','8','0');

INSERT INTO deudas
VALUES ('19','9','0');

INSERT INTO deudas
VALUES ('19','10','0');

INSERT INTO deudas
VALUES ('19','11','0');

INSERT INTO deudas
VALUES ('19','12','0');

INSERT INTO deudas
VALUES ('20','3','0');

INSERT INTO deudas
VALUES ('20','4','0');

INSERT INTO deudas
VALUES ('20','5','0');

INSERT INTO deudas
VALUES ('20','6','0');

INSERT INTO deudas
VALUES ('20','7','0');

INSERT INTO deudas
VALUES ('20','8','0');

INSERT INTO deudas
VALUES ('20','9','0');

INSERT INTO deudas
VALUES ('20','10','0');

INSERT INTO deudas
VALUES ('20','11','250');

INSERT INTO deudas
VALUES ('20','12','250');