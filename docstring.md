# Docstrings Summary

This file contains a compilation of all docstrings from the scripts in the Assets/Scripts folder.

## BackGroundManager.cs

### Start()

Khởi tạo chiều rộng của background bằng cách lấy kích thước của SpriteRenderer của midBg.
Thiết lập ban đầu cho hệ thống background, đảm bảo background có chiều rộng chính xác để cuộn mượt mà.

### Update()

Kiểm tra vị trí của camera chính và cập nhật vị trí background nếu cần thiết để tạo hiệu ứng cuộn.
Là vòng lặp chính để duy trì background cuộn theo chuyển động của camera, đảm bảo trải nghiệm người chơi mượt mà.

### UpdateBackgroundPosition(Vector3 direction)

Cập nhật vị trí của sideBg và hoán đổi vai trò giữa midBg và sideBg để tạo hiệu ứng cuộn background liên tục.
Xử lý logic di chuyển background khi camera vượt qua vị trí hiện tại, đảm bảo background không bị đứt đoạn..

## CollectFuel.cs

### OnTriggerEnter2D(Collider2D collision)

Kiểm tra va chạm với "Player", nếu có thì gọi hàm FillFuel() của FuelController và xóa đối tượng này.
Thu thập nhiên liệu khi người chơi va chạm với đối tượng này.
Cung cấp nhiên liệu cho người chơi và loại bỏ đối tượng sau khi thu thập, đảm bảo gameplay mượt mà.
Inputs: collision (Collider2D): Đối tượng va chạm với trigger này, được sử dụng để xác định nếu đó là "Player".

## CustomPerlin.cs

### Class CustomPerlin

Thuật toán Perlin Noise để tạo ra các giá trị noise mượt mà, tự nhiên
CustomPerlin cung cấp một phương thức Perlin01 để trả về giá trị noise trong khoảng [0,1] để dễ dàng tích hợp vào hệ thống sinh địa hình

### Fade(float t)

Làm mượt interpolation.

### Lerp(float a, float b, float t)

Nội suy tuyến tính.

### Gradient(int x)

Sinh gradient pseudo-random.

### Perlin1D(float x)

Sinh Perlin Noise 1D.

### Perlin01(float x)

Trả về Perlin Noise trong khoảng [0,1].

## DisplayDistance.cs

### Class DisplayDistance

Quản lý tính toán quãng đường di chuyển và hệ thống lưu trữ điểm cao nhất (High Score) cho trò chơi.
Cung cấp chức năng hiển thị quãng đường hiện tại và điểm cao nhất trên giao diện người dùng, đồng thời lưu trữ và quản lý danh sách điểm cao nhất một cách hiệu quả.

### Start()

start() được gọi một lần khi đối tượng này được kích hoạt.
Thiết lập vị trí bắt đầu của người chơi và tải dữ liệu điểm cao nhất từ bộ nhớ, đồng thời cập nhật giao diện người dùng với điểm cao nhất hiện tại.

### CalculateDistance()

Tính toán khoảng cách theo trục X và cập nhật lên giao diện người dùng.

### SaveCurrentScore()

Xử lý cập nhật điểm mới vào bảng xếp hạng.
Sử dụng Sort và duy trì kích thước danh sách cố định (Top 5).

### DisplayHighScore()

Cập nhật giá trị điểm cao nhất lên UI.

### SaveToMemory()

Chuyển đổi danh sách điểm sang chuỗi định dạng CSV để lưu vào PlayerPrefs.

### LoadTopScores()

Tải dữ liệu từ PlayerPrefs và giải mã chuỗi thành danh sách điểm cao.

## DriverDeathFromHead.cs

### OnCollisionEnter2D(Collision2D collision)

Kiểm tra va chạm với "Ground", nếu có thì gọi hàm GameOver() của GameManager.
Để Xử lý logic khi người lái va chạm với mặt đất và kết thúc trò chơi.

## EnvironmentGenerator.cs

### OnValidate()

OnValidate() được gọi mỗi khi có thay đổi trong Inspector.
Sinh dữ liệu địa hình mới và áp dụng lên SpriteShape để cập nhật ngay lập tức trong Editor
Cho phép xem kết quả của các thay đổi tham số ngay lập tức mà không cần phải chạy game

### GenerateTerrainData()

Sinh dữ liệu địa hình mới dựa trên Perlin Noise và các tham số đã thiết lập.
Tạo ra một danh sách các điểm Vector3 đại diện cho bề mặt địa hình rồi thêm 2 điểm chốt ở đáy để đóng kín Polygon.
Sử dụng Perlin Noise để tạo ra các điểm bề mặt tự nhiên và có thể nối tiếp nhau bằng cách sử dụng \_xOffset, đảm bảo tính liên tục của địa hình khi tạo nhiều đoạn khác nhau.

### ApplyDataToSpline()

Áp dụng dữ liệu địa hình đã sinh lên SpriteShapeController bằng cách chèn các điểm vào spline.
Xử lý logic để làm mượt các điểm bề mặt bằng cách đặt chế độ tangent là Continuous và thiết lập độ dài tangent dựa trên \_xMultiplier và \_curveSmoothness.
Đảm bảo rằng các điểm góc và đáy được đặt ở chế độ Linear để giữ cho các cạnh sắc nét, trong khi các điểm bề mặt được làm mượt để tạo ra một địa hình tự nhiên hơn.

## FuelController.cs

### Awake()

Thiết lập singleton instance để dễ dàng truy cập từ các script khác.

### Start()

Khởi tạo nhiên liệu hiện tại bằng với nhiên liệu tối đa và cập nhật giao diện người dùng.

### Update()

Cập nhật lượng nhiên liệu mỗi frame và kiểm tra xem nhiên liệu đã hết chưa.

### UpdateUI()

Cập nhật giao diện người dùng dựa trên lượng nhiên liệu hiện tại, bao gồm việc điều chỉnh fillAmount của Image và thay đổi màu sắc dựa trên Gradient.

### FillFuel()

Hồi xăng và tạo bình mới ở phía trước.

### SpawnNextFuel()

Tìm vị trí X ngẫu nhiên phía trước người chơi, bắn tia Ray từ trên trời xuống để tìm tọa độ Y của mặt đất, và tạo bình xăng mới tại điểm chạm mặt đất.
Nếu không tìm thấy đất (rơi vào hố), hàm sẽ tự gọi lại để thử tạo bình xăng ở một vị trí khác, đảm bảo rằng bình xăng luôn được tạo trên mặt đất và không bị lún xuống hố.

## GameManager.cs

### Class GameManager

Quản lý trạng thái và luồng điều khiển chính của trò chơi.

### Awake()

Khởi tạo Singleton và đặt tốc độ game về bình thường.

### GameOver()

Kích hoạt trạng thái kết thúc game: Hiện UI và dừng thời gian.

## GameOver.cs

### LoadMainMenu()

Lưu điểm và nhảy qua scene Menu

## LeaderBoardManager.cs

### RenderLeaderboard()

Đọc dữ liệu, xử lý chuỗi để tách thành mảng điểm số, sau đó tạo và cập nhật UI cho từng điểm số trong bảng xếp hạng.
Cung cấp cho người chơi một cái nhìn tổng quan về thành tích của họ so với các lần chơi trước đó hoặc so với những người chơi khác (nếu dữ liệu được chia sẻ).
Đảm bảo rằng dữ liệu trong PlayerPrefs được cập nhật đúng cách khi người chơi đạt được điểm số mới để bảng xếp hạng luôn phản ánh chính xác thành tích của họ.

## UIManager.cs

### Awake()

Khởi tạo Singleton để quản lý UI toàn cục, đảm bảo chỉ có một instance duy nhất của UIManager tồn tại trong suốt vòng đời của ứng dụng.
