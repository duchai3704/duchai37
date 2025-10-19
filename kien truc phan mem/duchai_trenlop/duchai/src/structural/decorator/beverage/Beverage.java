package structural.decorator.beverage;

public abstract class Beverage {
    String description;

    public Beverage(String description) {
        this.description = description;
    }

    public String getDescription() {
        return description;
    }

    public abstract int cost();
}
