using NetworkingLibrary;

public class PlayerInScene : BasePacket
{

    public bool inMainScene;

    public PlayerInScene()
    {

    }

    public PlayerInScene(bool inMainScene) : base(PacketType.PlayerInMainScenePacket)
    {
        this.inMainScene = inMainScene;
    }

    public new byte[] Serialize()
    {
        base.Serialize();
        binaryWriter.Write(inMainScene);
        FinishSerialization();
        return writeMemoryStream.ToArray();
    }

    public new PlayerInScene Deserialize(byte[] _dataToDeserialize, int index)
    {
        base.Deserialize(_dataToDeserialize, index);
        inMainScene = binaryReader.ReadBoolean();
        return this;
    }
}
