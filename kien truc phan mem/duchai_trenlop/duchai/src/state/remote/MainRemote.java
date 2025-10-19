package state.remote;

public class MainRemote {
    public static void main(String[] args) {
        RemoteControl remote = new RemoteControl();
        System.out.println("Lần ấn đầu tiên: ");
        remote.powerPress();
        System.out.println("Lần ấn thứ hai: ");
        remote.powerPress();
    }
}
