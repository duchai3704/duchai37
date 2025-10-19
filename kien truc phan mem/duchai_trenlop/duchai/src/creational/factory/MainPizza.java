package creational.factory;

import creational.factory.vietnam.VietNamPizzaStore;

public class MainPizza {
    public static void main(String[] args) {
        PizzaStore ps = new VietNamPizzaStore();
        Pizza p = ps.orderPizza(PizzaType.HAISAN);
        System.out.println(p.toString());
    }
}
