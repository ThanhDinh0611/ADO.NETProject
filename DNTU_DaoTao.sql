--Tạo Database
create Database DNTU_DaoTao
go

use DNTU_DaoTao
go


--Tạo bảng
create table STUDENTS(
	MASV nvarchar(50) primary key,
	Hoten nvarchar(50),
	Email nvarchar(50),
	SoDT nvarchar(50),
	Gioitinh bit,
	DiaChi nvarchar(50),
	Hinh nvarchar(50)
)
go

create table GRADE(
	ID int identity(1,1) primary key,
	MASV nvarchar(50) unique,
	Tienganh float,
	Tinhoc float,
	GDTC float
)
go

create table USERS(
	username nvarchar(50) primary key,
	password nvarchar(50),
	role nvarchar(50)
)
go

--Tạo Constraint
alter table GRADE add constraint FK_GRADE_STUDENTS foreign key (MASV) references STUDENTS(MASV)
go


--Nhập Users
insert USERS values('admin','admin','admin')
insert USERS values('test','123456','admin')
insert USERS values('teacher1','123456','teacher')
insert USERS values('teacher2','123456','teacher')
go


--Nhập STUDENTS
insert STUDENTS values('SV01',N'Nguyễn Văn A','nguyenvana@gmail.com','0123456789',0,N'12 Đinh Tiên Hoàng, TPHCM','Resources/nguyenvana.jpg')
insert STUDENTS values('SV02',N'Nguyễn Văn B','nguyenvanb@gmail.com','0123456777',0,N'5 Võ Thị Sáu, Biên Hòa','Resources/nguyenvanb.jpg')
insert STUDENTS values('SV03',N'Nguyễn Thị C','nguyenthic@gmail.com','0987654321',1,N'81 Võ Thị Sáu, Biên Hòa','Resources/nguyenthic.jpg')
go

--Nhập Grade
select * from GRADE
insert GRADE values('SV01',7,8,9)
insert GRADE values('SV02',6,6,9)
insert GRADE values('SV03',10,9,9)
go

