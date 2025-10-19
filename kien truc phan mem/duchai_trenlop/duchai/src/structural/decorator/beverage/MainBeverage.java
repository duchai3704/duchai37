package structural.decorator.beverage;

public class MainBeverage {
    public static void main(String[] args) {
        Beverage b = new Espresso("King Cafe Espresso");
        b = new Milk("Sữa cô gái hà lan",b);
        b = new Mocha("MoCha", b);
        System.out.println(b.cost());
        System.out.println(b.getDescription());
    }
}
