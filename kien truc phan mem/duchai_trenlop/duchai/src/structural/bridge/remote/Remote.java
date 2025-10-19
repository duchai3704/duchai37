package structural.bridge.remote;

public class Remote {
    Device device;

    public Remote(Device device) {
        this.device = device;
    }

    public void togglePower() {
        if (this.device.isEnable()) {
            this.device.disable();
        } else {
            this.device.enable();
        }

    }

    public void volumeDown() {
        int volume = this.device.getVolume();
        if (volume > 0) {
            --volume;
            this.device.setVolume(volume);
            System.out.println(volume);
        }

    }

    public void volumeUp() {
        int volume = this.device.getVolume();
        if (volume < 100) {
            ++volume;
            this.device.setVolume(volume);
            System.out.println(volume);
        }

    }
}
