--hello
--E'lon qilish

CREATE TABLE "Group"(
    "id" serial NOT NULL,
    "direction" TEXT NOT NULL,
    "teacherName" TEXT NOT NULL
);
ALTER TABLE
    "Group" ADD PRIMARY KEY("id");
CREATE TABLE "student"(
    "id" serial NOT NULL,
    "name" TEXT NOT NULL,
    "age" INTEGER NOT NULL
);
ALTER TABLE
    "student" ADD PRIMARY KEY("id");
CREATE TABLE "subject"(
    "id" serial NOT NULL,
    "name" TEXT NOT NULL,
    "authorName" TEXT NOT NULL
);
ALTER TABLE
    "subject" ADD PRIMARY KEY("id");
CREATE TABLE "studentANDgroup"(
    "studentId" serial NOT NULL,
    "groupId" INTEGER NOT NULL
);
ALTER TABLE
    "subject" ADD CONSTRAINT "subject_groupid_foreign" FOREIGN KEY("id") REFERENCES "Group"("id");
ALTER TABLE
    "studentANDgroup" ADD CONSTRAINT "studentandgroup_groupid_foreign" FOREIGN KEY("groupId") REFERENCES "Group"("id");
ALTER TABLE
    "studentANDgroup" ADD CONSTRAINT "studentandgroup_studentid_foreign" FOREIGN KEY("studentId") REFERENCES "student"("id");
	
	

--ma'lumot bilan to'ldirish


insert into "Group"(direction,"teacherName")values
('Biologiya va Farmaseftika','Bekmurod'),
('Fizika va Astronomiya','Olimjon'),
('Amaliy Matematika','Olmas'),
('Tarix','Muxriddin'),
('Chizmachilik va sanat','Shanoza'),
('Kimyo va Texnika','Komila'),
('Ona-tili va Adabiyot','Azamat'),
('Rus-tili - chet tili','Malika'),
('Tarbiya va Axloq','Fozil'),
('Informatika va axborot','Iftihor'),
('Jismoniy-Tarbiya','Sardor');

insert into subject(name,"authorName")values
('Biologiya','Zarina'),
('Fizika','Lola'),
('Matematika','Samandar'),
('Tarix','Turdivoy'),
('Chizmachilik','Tursunboy'),
('Kimyo','Ibrohim'),
('Ona-tili','Marguba'),
('Rus-tili','Shahlo'),
('Tarbiya','Aldiyor'),
('Informatika','Nodira'),
('Jismoniy-Tarbiya','Nodir');


insert into student(name,age)values
('Abduxoliq',17),
('Abdullox',18),
('Otabek',16),
('Izzat',17),
('Farrux',16),
('Abdulbasit',18),
('Abdurrohman',18),
('Shahriyor',17),
('Said',20),
('Mansur',21),
('Jahongir',19),
('Ismoil',15),
('Usmon',19);

insert into "studentANDgroup"("studentId","groupId")values
(1,1),
(1,2),
(1,3),
(1,4),
(2,5),
(3,5),
(4,5),
(5,5),
(6,7),
(2,6),
(9,8),
(13,11),
(11,2),
(3,3),
(13,2),
(6,11),
(6,11),
(6,11),
(13,2);


--trigger funksiyasi uchun tablear yasash
	--student ushun
		create table studentArchive(id int,name varchar(255),age int,vaqt varchar(244));
	--grup uschun
		create table groupArchive(id int,direction varchar(255),teacherName varchar(222),vaqt varchar(244));
	--subject uchun
		create table subjectArchive(id int,name varchar(333),authorName varchar(333),vaqt varchar(333));
	--studentANDgroup uchun
		create table studentANDgroupArchive(studentId int,groupId int,vaqt varchar(444));
		

--funksiya yasash

	--studentArchive uchun
		create or replace function studentArchiving()
			returns trigger
			language plpgsql
			AS
			$$
			begin
				if TG_OP='DELETE' then
					insert into studentArchive(id,name,age,vaqt)
					values(old.id,old.name,old.age,now());
					return old;
				elseif TG_OP='UPDATE' then
					insert into studentArchive(id,name,age,vaqt)
					values(old.id,old.name,old.age,now());
					return new;
				end if;
			end;
			$$;
	
	--groupArchive uchun
		create or replace function groupArchiving()
			returns trigger
			language plpgsql
			AS
			$$
			begin
				if TG_OP='DELETE' then
					insert into groupArchive(id,direction,teacherName,vaqt)
					values(old.id,old.direction,old."teacherName",now());
					return old;
				elseif TG_OP='UPDATE' then
					insert into groupArchive(id,direction,teacherName,vaqt)
					values(old.id,old.direction,old."teacherName",now());
					return new;
				end if;
			end;
			$$;
	
	--subjectArchive uchun
		create or replace function subjectArchiving()
			returns trigger
			language plpgsql
			AS
			$$
			begin
				if TG_OP='DELETE' then
					insert into subjectArchive(id,name,authorName,vaqt)
					values(old.id,old.name,old."authorName",now());
					return old;
				elseif TG_OP='UPDATE' then
					insert into subjectArchive(id,name,authorName,vaqt)
					values(old.id,old.name,old."authorName",now());
					return new;
				end if;
			end;
			$$;
	
	--studentANDgroupArchive uchun
		create or replace function studentANDgroupArchiving()
			returns trigger
			language plpgsql
			AS
			$$
			begin
				if TG_OP='DELETE' then
					insert into studentANDgroupArchive(studentId,groupId,vaqt)
					values(old."studentId",old."groupId",now());
					return old;
				elseif TG_OP='UPDATE' then
					insert into studentANDgroupArchive(studentId,groupId,vaqt)
					values(old."studentId",old."groupId",now());
					return new;
				end if;
			end;
			$$;
		
		
--Triggerlar e'lon qilish
	--student uchun
		create trigger studentTrigger
			before Update or delete
			on student
			for each row
			execute function studentArchiving();
	
	--group uchun
		create trigger groupTrigger
			before Update or delete
			on "Group"
			for each row
			execute function groupArchiving();
			
	--subject uchun
		create trigger subjectTrigger
			before Update or delete
			on subject
			for each row
			execute function subjectArchiving();
			
	--studentANDgroup uchun 
		create trigger studentANDgroupTrigger
			before Update or delete
			on "studentANDgroup"
			for each row
			execute function studentANDgroupArchiving();

--buglarsiz ishlamoqda


--!!!!!!!!!!
--Pastda endi hohlaganingizcha update yoki delete qilishingiz mumkin
--faqat column nameda KATTA harf ishlatilgan bo'lsa uni qoshtirnoqga olib yozishingiz kerak-> "ColumnName"


--agar qandaydir hatoga yo'l qoygan bo'lsam meni ogohlantring, tog'irlab qoyaman
