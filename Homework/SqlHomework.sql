3.1 Exersize
	a) select title from course where credits=3 and dept_name = 'Comp. Sci.';
	c) select max(salary) highest_salary from instructor;
	d) select * from instructor where salary=(select max(salary) from instructor);

3.3 Exersize
	a) update instructor
	   set salary=salary/10+salary
	   where dept_name='Comp. Sci.';

--hozircha shu, qoganini yana deadline gacha qoshib boraman
--hozir kolejga ketvotudm iloji bosa 14: 30 gacha uy ishimi boholami turin 🙏)
