INSERT INTO Books (Id, Title, Author, Description, Price, Stock, CreatedAt, CategoryId)
VALUES
-- Kỹ thuật (CategoryId = 4)
(NEWID(), N'Lập Trình C# Cơ Bản', N'Nguyễn Văn A', N'Sách học lập trình C# cho người mới bắt đầu', 120000, 20, GETUTCDATE(), 4),
(NEWID(), N'ASP.NET Core Web API', N'Trần Minh Khang', N'Hướng dẫn xây dựng RESTful API với .NET Core', 180000, 15, GETUTCDATE(), 4),
(NEWID(), N'Cấu Trúc Dữ Liệu Và Giải Thuật', N'Lê Hoàng Nam', N'Kiến thức nền tảng về thuật toán và cấu trúc dữ liệu', 150000, 10, GETUTCDATE(), 4),
(NEWID(), N'SQL Server Thực Chiến', N'Phạm Quốc Huy', N'Hướng dẫn sử dụng SQL Server từ cơ bản đến nâng cao', 170000, 12, GETUTCDATE(), 4),

-- Kinh tế (CategoryId = 5)
(NEWID(), N'Cha Giàu Cha Nghèo', N'Robert Kiyosaki', N'Cuốn sách nổi tiếng về tư duy tài chính', 95000, 25, GETUTCDATE(), 5),
(NEWID(), N'Dạy Con Làm Giàu', N'Shinjiro', N'Bài học quản lý tài chính cá nhân', 110000, 18, GETUTCDATE(), 5),
(NEWID(), N'Kinh Tế Học Vi Mô', N'Nguyễn Hải Đăng', N'Kiến thức kinh tế học cơ bản', 145000, 8, GETUTCDATE(), 5),
(NEWID(), N'Tư Duy Nhanh Và Chậm', N'Daniel Kahneman', N'Phân tích hành vi và tâm lý kinh tế', 160000, 14, GETUTCDATE(), 5),

-- Văn học (CategoryId = 6)
(NEWID(), N'Tắt Đèn', N'Ngô Tất Tố', N'Tác phẩm văn học hiện thực Việt Nam', 85000, 30, GETUTCDATE(), 6),
(NEWID(), N'Lão Hạc', N'Nam Cao', N'Truyện ngắn nổi tiếng của Nam Cao', 70000, 22, GETUTCDATE(), 6),
(NEWID(), N'Đắc Nhân Tâm', N'Dale Carnegie', N'Nghệ thuật giao tiếp và ứng xử', 125000, 16, GETUTCDATE(), 6),
(NEWID(), N'Nhà Giả Kim', N'Paulo Coelho', N'Hành trình theo đuổi ước mơ', 99000, 20, GETUTCDATE(), 6),

-- Thiếu nhi (CategoryId = 7)
(NEWID(), N'Doraemon Tập 1', N'Fujiko F. Fujio', N'Truyện tranh thiếu nhi nổi tiếng', 25000, 50, GETUTCDATE(), 7),
(NEWID(), N'Conan Tập 5', N'Aoyama Gosho', N'Truyện tranh trinh thám dành cho thiếu nhi', 30000, 45, GETUTCDATE(), 7),
(NEWID(), N'Truyện Cổ Tích Việt Nam', N'Nhiều tác giả', N'Tổng hợp truyện cổ tích dành cho trẻ em', 80000, 17, GETUTCDATE(), 7),

-- Tâm lý (CategoryId = 8)
(NEWID(), N'Tâm Lý Học Hành Vi', N'Brian Tracy', N'Khám phá hành vi và suy nghĩ con người', 140000, 11, GETUTCDATE(), 8),
(NEWID(), N'Hiểu Về Trái Tim', N'Minh Niệm', N'Sách chữa lành và phát triển bản thân', 115000, 19, GETUTCDATE(), 8),
(NEWID(), N'Không Diệt Không Sinh Đừng Sợ Hãi', N'Thích Nhất Hạnh', N'Bài học về cuộc sống và bình an', 105000, 13, GETUTCDATE(), 8);