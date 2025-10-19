package creational.builder;

public class MainComputer {
    public static void main(String[] args) {
        Computer computer = new Computer.Builder()
                .buildCPU("Intel Core i9")
                .buildRAM("64GB 720MHz")
                .buildStorage("4GB SSD NVMe")
                .buildScreen("24 inches 8k")
                .build();
        System.out.println(computer.toString());
    }
}
