using UnityEngine;

namespace MIRAI.Grid.Cell.Registrars
{

    public class TowerRegistrar : MonoBehaviour
    {
        public static TowerRegistrar Instance { get; private set; }

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