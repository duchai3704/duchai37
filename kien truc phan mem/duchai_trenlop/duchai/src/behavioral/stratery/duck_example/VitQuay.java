package behavioral.stratery.duck_example;

public class VitQuay extends Duck{

    public VitQuay(IFlyBehavior flyBehavior, IQuackBehavior quackBehavior) {
        super(flyBehavior, quackBehavior);
    }

    @Override
    public void display() {
        System.out.println("Vit Quay Bac Kinh!");
        swim();
        performFly();
        performQuack();
    }
}
