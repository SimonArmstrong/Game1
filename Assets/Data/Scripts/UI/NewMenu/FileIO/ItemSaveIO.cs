using UnityEngine;

public static class ItemSaveIO {
    private static readonly string baseSavePath;

    static ItemSaveIO(){
        baseSavePath = Application.persistentDataPath;
    }

    public static void SaveItems(TInv.ContainerSaveData items, string path){
        FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + path + ".dat", items);
    }

    public static TInv.ContainerSaveData LoadItems(string path){
        string filePath = baseSavePath + "/" + path + ".dat";

        if(System.IO.File.Exists(filePath)){
            return FileReadWrite.ReadFromBinaryFile<TInv.ContainerSaveData>(filePath);
        }
        return null;
    }

    public static void SaveItems(ItemContainerSaveData items, string path)
    {
        FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + path + ".dat", items);
    }

    public static ItemContainerSaveData _LoadItems(string path)
    {
        string filePath = baseSavePath + "/" + path + ".dat";

        if (System.IO.File.Exists(filePath))
        {
            return FileReadWrite.ReadFromBinaryFile<ItemContainerSaveData>(filePath);
        }
        return null;
    }
}
