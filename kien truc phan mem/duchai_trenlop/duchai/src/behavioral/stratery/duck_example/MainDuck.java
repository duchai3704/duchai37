package behavioral.stratery.duck_example;

public class MainDuck {
    public static void main(String[] args) {
        Duck d = new VitQuay(new BayVong(), new SQuack());
        d.setQuackBehavior(new QuackNew());
        d.display();
    }
}
