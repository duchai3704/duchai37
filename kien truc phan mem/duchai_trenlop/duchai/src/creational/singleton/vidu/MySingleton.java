package creational.singleton.vidu;

public class MySingleton {
    //b1 khai bao biến tĩnh Singleton
    private static MySingleton instance;
    int count = 0;
    //b2 Cài đặt lớp khởi tạo có bổ từ truy cập private hoặc protected
    private MySingleton(){}
    //b3 Cài đặt phương thức tĩnh để trả về biến instance
    public static MySingleton getInstance(){
        if (instance == null)
            instance = new MySingleton();
        return instance;
    }
    public void display(){
        System.out.println(++count);
    }
}
