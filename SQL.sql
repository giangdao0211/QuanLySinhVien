create database QLDiem
go
use QLDiem
--xoa
use master
ALTER DATABASE QLDiem SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
drop database QLDiem
--xoa
create table TAIKHOAN(
	userName varchar(30) primary key not null,
	passwords varchar(16) not null 
)
create table KHOA(
	maKhoa varchar(30) primary key not null,
	tenKhoa nvarchar(50) not null 
)
create table SINHVIEN(
	maSV varchar(10) primary key not null,
	tenSV nvarchar(32) not null,
	ngaySinh Date not null,
	gioiTinh nvarchar(5) not null,
	diaChi nvarchar(50) not null,
	soDT varchar(10) not null,
	email varchar(30) not null,
	userName varchar(30) not null,
	foreign key (userName) references TAIKHOAN(userName)
)
create table GIAOVIEN(
	maGV varchar(30) primary key not null,
	hoTen nvarchar(32) not null,
	ngaySinh Date not null,
	queQuan nvarchar(50) not null,
	soDT varchar(10) not null,
	trinhDoHocVan nvarchar(20) not null,
	userName varchar(30) not null
	foreign key (userName) references TAIKHOAN(userName),
	maKhoa varchar(30) not null
	foreign key (maKhoa) references KHOA(maKhoa)
)
create table MONHOC(
	maMH varchar(10) primary key not null,
	tenMH nvarchar(32) not null,
	lyThuyet int,
	thucHanh int,
	soTC int,
	ky int
)
create table DIEM(
	maMH varchar(10) not null,
	maSV varchar(10) not null,
	diemTX1 float,
	diemTX2 float,
	diemThi float,
	diemTKSo float,
	diemTKChu varchar(2),
	constraint pk_Diem primary key (maMH, maSV),
	foreign key (maMH) references MONHOC(maMH),
	foreign key (maSV) references SINHVIEN(maSV),
)
create table LOP(
	maLop varchar(10) primary key not null,
	tenLop nvarchar(32) not null,
	maGV varchar(30),
	maKhoa varchar(30),
	maMH varchar(10),
	ngayBatDauLopHoc date,
	NgayKetThucLopHoc date,
	foreign key (maGV) references GIAOVIEN(maGV),
	foreign key (maKhoa) references KHOA(maKhoa),
	foreign key (maMH) references MONHOC(maMH),
)

create table LOPVASINHVIEN(
	maLop varchar(10),
	maSV varchar(10),
	ngayBatDauLopHoc Date,
	ngayKetThucLopHoc Date,
	constraint pk_LOP_SV primary key (maLop, maSV),
	foreign key (maLop) references LOP(maLop),
	foreign key (maSV) references SINHVIEN(maSV)
)

insert into TAIKHOAN values ('chung','123123'),
							('cuong','123123'),
							('dao','123123'),
							('cong','123123'),
							('hiep','123123'),
							('nghien','123123')
insert into SINHVIEN values ('2021000001',N'Nguyễn Đức Chung','2003-01-01',N'Nam',N'Hà Nội','0987654321','chung@gmail.com','chung'),
							('2021000002',N'Võ Mạnh Cường','2003-01-02',N'Nữ',N'TP. Hồ Chí Minh','0987654322','cuong@gmail.com','cuong'),
							('2021000003',N'Giang Quang Đạo','2003-02-03',N'Nam',N'Hà Nội','0987654345','dao@gmail.com','dao'),
							('2021000004',N'Quách Thành Công','2003-03-04',N'Nữ',N'Bắc Ninh','0987654567','cong@gmail.com','cong')
insert into MONHOC values ('20231IT601',N'Thực tập cơ sở ngành',2,1,3,5),
							('20231IT602',N'Thiết kế Web',0,3,3,4),
							('20231IT603',N'Trí tuệ nhân tạo',3,0,3,3),
							('20231IT604',N'Thiết kế đồ họa 2D', 2,1,3,5)
insert into KHOA values ('CNTT',N'Khoa công nghệ thông tin'),
						('KT',N'Kế toán')
insert into GIAOVIEN values ('GV1',N'Phạm Văn Hiệp','1999-04-05',N'Hà Nội','0123456789',N'Thạc sĩ','hiep','CNTT'),
							('GV2',N'Nguyễn Bá Nghiễn','1999-04-06',N'Hà Nội','0912345678',N'Thạc sĩ','nghien','CNTT')
insert into DIEM values ('20231IT601','2021000001',9,9,9,9,'A'),
						('20231IT602','2021000002',10,10,10,10,'A'),
						('20231IT603','2021000003',8,8,8,8,'B+'),
						('20231IT603','2021000004',7,7,7,7,'B')
insert into LOP values ('004',N'Lớp TTCSN 4','GV1','CNTT','20231IT601','2023-9-11','2023-12-25'),
						('005',N'Lớp TKW 5','GV2','CNTT','20231IT602','2023-9-11','2023-12-25'),
						('006',N'Lớp TNTT 6','GV2','CNTT','20231IT603','2023-9-11','2023-12-25')
insert into LOPVASINHVIEN values ('004','2021000001','2023-9-11','2023-12-25'),
								('005','2021000002','2023-9-11','2023-12-25'),
								('006','2021000003','2023-9-11','2023-12-25')