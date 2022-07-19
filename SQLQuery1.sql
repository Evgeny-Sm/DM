select * from Persons
select * from FileUnits
select * from Projects
select * from Worker
select * from Departments

truncate table Persons
truncate table FileUnits
truncate table Projects
truncate table Worker
truncate table Departments

delete Persons where id=15
delete Departments where Name='CO'
truncate table FileUnits
truncate table Projects
truncate table Worker
truncate table Departments

drop table Persons
drop table FileUnits
drop table Projects
drop table Worker
drop table Departments
