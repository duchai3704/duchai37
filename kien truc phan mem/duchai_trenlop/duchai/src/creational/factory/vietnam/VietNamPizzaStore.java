package creational.factory.vietnam;

import creational.factory.Pizza;
import creational.factory.PizzaStore;
import creational.factory.PizzaType;

public class VietNamPizzaStore extends PizzaStore {
    @Override
    public Pizza createPizza(PizzaType type) {
        switch (type){
            case HAISAN -> {
                return new PizzaHaiSan();
            }
            case BO -> {
                return new PizzaBo();
            }
        }
        return new PizzaHaiSan();
    }
}
