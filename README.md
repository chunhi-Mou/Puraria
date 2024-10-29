# Puraria
 
Nội dung Branch:
- Tạo được layout cho map (Bấm "Generate Map" trong Inspector để Update Map khi thay đổi size Map)
- 1 ô Đất sẽ dựa vào Scriptable Obj được kéo vào để tham chiếu, chuyển Sprite tương ứng:
  + Hiện tại đang cho mặc định là đất tốt (chỉ hiện ra khi Runtime)
  + Trong Editor là hiện khung Hex Trắng
- Ngoài ra các tâm đã được lưu trong mảng 2 chiều `vertices` để sau cài đặt thử A*
