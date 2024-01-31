--Tablelar E'lon qilish
create table Animals(
	id serial primary key,
	name text,
	age int,
	breed text,
	adoptionStatus text);
	
create table PotentialAdopters(
	id serial primary key,
	name text,
	contactInfo name,	
	homeEnDetails text);
	
create table AdoptionTransactions(
	id serial primary key,
	dateAdoption text,
	adoptedPet int unique references Animals(id),
	adopter int references PotentialAdopters(id),
	transactionStatus text);
	
create table MedicalRecords(
	id serial primary key unique references Animals(id),
	vaccinationStatus text,
	lastVisitDate text);
	
--Ma'lumot qo'shish
insert into Animals(name,age,breed,adoptionStatus)values
('Bori bosar',3,'kuchuk','pending'),
('Mosh',1,'mushuk','adopted'),
('Bezbet',3,'mushuk','available'),
('Olapar',5,'kuchuk','pending'),
('Belka',6,'sichqon','available'),
('Basel',4,'mushuk','pending'),
('Qashqir',2,'kuchuk','available'),
('Tom',1,'mushuk','adopted'),
('Nortoy',4,'sichqon','pending'),
('Rio',5,'toti','adopted'),
('Tilla',7,'baliq','pending'),
('Gisht',3,'toshbaqa','available'),
('Dora',1,'baliq','adopted'),
('Ratatuy',3,'sichqon','pending'),
('Jerry',3,'toti','adopted'),
('Qoravoy',3,'mushuk','available');


insert into PotentialAdopters(name,contactInfo,homeEnDetails)values
('Abduxoliq','998935302210','keng maydonli yerim bor'),
('Ali','998935215465','domda turaman, uyim isssiqgina'),
('Ibrohim','998946273842','uy hayvonlari uchun kichkinagina uycham bor'),
('Orif','998994328943','hovilmda boqaman'),
('Ilhom','998954879219','yertolamda boqaman'),
('Iskandar','998719401490','kotta chimli polyam bor'),
('Nuriddin','998338392193','tomorqamda boqaman hayvonlani'),
('Nurkent','998990329103','kichkinagina uchaskada turaman'),
('Abduhalil','998907392183','hayvonlar uchun oyingohim bor'),
('Ziyodilla','998934324323','bolxonada boqaman');

insert into AdoptionTransactions(dateAdoption,adoptedPet,adopter,transactionStatus)values
('12-12-2023',1,3,'good'),
('21-03-2022',2,3,'great'),
('13-02-2021',3,2,'successfull'),
('21-02-2023',4,5,'good'),
('23-09-2023',5,6,'great'),
('14-08-2023',6,9,'nice'),
('06-01-2022',7,1,'great'),
('04-02-2023',8,10,'great'),
('14-11-2023',9,3,'successfull'),
('05-11-2020',10,4,'normal'),
('31-06-2019',11,5,'good'),
('30-03-2023',12,7,'great'),
('08-06-2023',13,9,'good');


insert into MedicalRecords(vaccinationStatus,lastVisitDate)values
('emlangan','23-02-2023'),
('emlanmagan','03-02-2020'),
('emlangan','03-05-20231'),
('emlanmagan','12-06-2022'),
('emlangan','22-06-2021'),
('emlanmagan','31-11-2023'),
('emlangan','29-12-2023'),
('emlangan','09-12-2021'),
('emlanmagan','09-12-2019'),
('emlangan','23-02-2021'),
('emlangan','12-07-2022'),
('emlanmagan','21-08-2022'),
('emlangan','22-11-2021'),
('emlangan','24-04-2019'),
('emlanmagan','15-05-2019'),
('emlangan','17-02-2020');

--funksiyalar yasash
create or replace function avg_age_by_breed(turi varchar)
returns table(yosh real)
language plpgsql as
$$
begin
	return query select avg(age)::real ortacha_yoshi from Animals where breed=turi;
end
$$;

create or replace function pets_count_of_adopter(adoper int)
returns table(count bigint)
language plpgsql as
$$
begin
	return query select count(*) from AdoptionTransactions where adopter=adoper;
end
$$;

create or replace function generate_medical_records_of_pet(petId int)
returns table(status text,lastvisit text)
language plpgsql as
$$
begin
	return query select vaccinationStatus,lastVisitDate from MedicalRecords where id=petId;
end
$$;


--quyidagi funksiyalarni ishlatib korishinigiz mumkin

--select avg_age_by_breed('mushuk');
--select pets_count_of_adopter(5);
--select generate_medical_records_of_pet(1);



-- Agar qandaydir xatoga yo'l qoygan bo'lsam meni ogohlatring, to'g'irlab qo'yaman)
