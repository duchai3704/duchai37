package structural.decorator.beverage;

public class Mocha extends CondimentDecorator{
    public Mocha(String description, Beverage beverage) {
        super(description, beverage);
    }

    @Override
    public int cost() {
        return 27000 + beverage.cost();
    }
}
