using UnityEngine;

namespace MIRAI.Grid.Cell
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridCellShell : MonoBehaviour, ISubscriber
    {
        #region PARAMETERS

        [SerializeField]
        private ShellTooltip _shellTooltip;
        public ShellTooltip ShellTooltip => _shellTooltip;

        #endregion

        #region COMPONENTS
        protected SpriteRenderer _renderer;
        #endregion

        protected bool IsSelected;
        
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public void SetXY(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void SetTooltip(ShellTooltip shellTooltip)
            =>_shellTooltip = shellTooltip;

        protected virtual void Awake()
        {
            GridInteractor.OnSelectedHandler += OnAnyShellSelected;
            _renderer = GetComponent<SpriteRenderer>();
        }

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
        public GridCellShell InstantiateOther(GridCellShell other, Transform tilesParent) 
            => Instantiate(other, tilesParent);

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