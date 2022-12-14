select * from Persons
select * from FileUnits
select * from Projects
select * from UserProfiles
select * from Departments
select * from UserActions
select * from Controls

Update Controls set IsInAction=0 where id=140


Update FileUnits set Status='Work' where id=137

Update Projects set Name='Санаторий Волна.Литер В' where id=3

truncate table Persons
truncate table FileUnits
truncate table Projects
truncate table Workers
truncate table Departments

delete from FileUnits
delete from Challenges
delete from Projects
delete from Messages

delete from Controls

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
