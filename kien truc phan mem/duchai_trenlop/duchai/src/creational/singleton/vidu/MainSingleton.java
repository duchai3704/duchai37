package creational.singleton.vidu;

public class MainSingleton {
    public static void main(String[] args) {
        MySingleton s1 = MySingleton.getInstance();
        MySingleton s2 = MySingleton.getInstance();
        s1.display(); //1
        s2.display(); //2
        s1.display(); //3
        s2.display(); //4
    }
}
