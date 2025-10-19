package structural.bridge.remote;

public class TV implements Device {
    boolean state = false;
    int volume = 0;

    public TV() {
    }

    public boolean isEnable() {
        return this.state;
    }

    public void enable() {
        this.state = true;
        System.out.println("Turn on");
    }

    public void disable() {
        this.state = false;
        System.out.println("Turn off");
    }

    public int getVolume() {
        return this.volume;
    }

    public void setVolume(int percent) {
        this.volume = percent;
    }
}
