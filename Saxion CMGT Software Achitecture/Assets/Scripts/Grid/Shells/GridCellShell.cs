using UnityEngine;

namespace MIRAI.Grid.Cell
{
    public class GridCellShell : MonoBehaviour, ISubscriber
    {
        #region PARAMETERS

        [SerializeField]
        private CellDisplayData _shellData;
        public CellDisplayData ShellData => _shellData;

        #endregion

        protected bool IsSelected;
        
        protected int X;
        protected int Y;

        public void SetXY(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected virtual void Awake() 
            => GridInteractor.OnSelectedHandler += OnAnyShellSelected;

        public virtual void Subscribe()
            => GridInteractor.OnSelectedHandler += OnAnyShellSelected;
        public virtual void Unsubscribe()
            => GridInteractor.OnSelectedHandler -= OnAnyShellSelected;

        protected virtual void OnAnyShellSelected(GridCellShell shell)
        {
            if (shell != this)
            {
                if (IsSelected) 
                    OnDeselected();
            }
            else 
            OnSelected();
        }

        protected virtual void OnSelected() => IsSelected = true;
        protected virtual void OnDeselected() => IsSelected = false;

        public void DestroySelf() => Destroy(gameObject);

        protected virtual void OnDestroy()
        {
            Unsubscribe();

            if (!IsSelected)
                return;

            OnDeselected();
            GridInteractor.Deselect();
        }

    }
}