package behavioral.stratery.duck_example;

public class SQuack implements IQuackBehavior{
    @Override
    public String quack() {
        return "Quac, quac, quac...!";
    }
}
