package creational.factory.vietnam;

import creational.factory.Pizza;

public class PizzaBo extends Pizza {
    @Override
    public void prepare() {
        this.getBuilder().append("Chuẩn bị bột, bơ");
    }
    @Override
    public void bake() {
        this.getBuilder().append("\n Nướng trên lò than 20 phút");
    }

    @Override
    public void cut() {
        this.getBuilder().append("\n Cắt làm 4 miếng");
    }

    @Override
    public void box() {
        this.getBuilder().append("\n Gói bằng lá chuối, bỏ vào hộp");
    }
}
