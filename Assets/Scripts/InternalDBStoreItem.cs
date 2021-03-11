using UnityEngine;

public class InternalDBStoreItem : MonoBehaviour
{
	public void populateDB()
	{
		this.gameObject.transform.GetComponent<DatabaseHandler>().storeItemList.Add(new StoreItem {name = "ChristmasPack2019", price = 10, image = "", info = "christmastesttext", messageText = "test1"});
		this.gameObject.transform.GetComponent<DatabaseHandler>().storeItemList.Add(new StoreItem {name = "HalloweenPack2019", price = 10, image = "", info = "halloweentesttext", messageText = "test2"});
		
	}
}