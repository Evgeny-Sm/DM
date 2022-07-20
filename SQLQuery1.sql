select * from Persons
select * from FileUnits
select * from Projects
select * from UserProfiles
select * from Departments
select * from UserActions


truncate table Persons
truncate table FileUnits
truncate table Projects
truncate table Workers
truncate table Departments

delete Persons where id=15
delete Departments where Name='CO'
truncate table FileUnits
truncate table Projects
truncate table Workers
truncate table Departments

drop table Persons
drop table FileUnits
drop table Projects
drop table UserProfiles
drop table Departments
