using UnityEngine;

namespace MIRAI.Grid.Cell
{
    public class TowerBuildingShop : MonoBehaviour
    {
        public static TowerBuildingShop Instance { get; private set; }

        #region PARAMETERS
        [SerializeField]
        private TowerBuildingShopEntry[] _slots;
        public TowerBuildingShopEntry[] Slots => _slots;
        #endregion
        
        private void Awake()
        {
            if (Instance is not null)
                Destroy(this);
            else
                Instance = this;

            DontDestroyOnLoad(this);
        }
    }
}