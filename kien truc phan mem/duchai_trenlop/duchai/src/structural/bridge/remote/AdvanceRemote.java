package structural.bridge.remote;

public class AdvanceRemote extends Remote {
    int currentVolume;

    public AdvanceRemote(Device device) {
        super(device);
    }

    public void mute() {
        if (this.device.getVolume() == 0) {
            this.device.setVolume(this.currentVolume);
            System.out.println(this.currentVolume);
        } else {
            this.currentVolume = this.device.getVolume();
            this.device.setVolume(0);
            System.out.println(this.device.getVolume());
        }

    }
}
