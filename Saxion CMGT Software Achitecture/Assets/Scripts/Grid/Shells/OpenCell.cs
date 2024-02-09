using UnityEngine;

namespace MIRAI.Grid.Cell
{
    [RequireComponent(typeof(Animator))]
    public class OpenCell : GridCellShell
    {
        protected Animator Animator;

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
            Subscribe();
        }

        protected override void OnSelected()
        {
            base.OnSelected();
            Animator.SetTrigger("Selected");
        }
        protected override void OnDeselected()
        {
            base.OnDeselected();
            Animator.SetTrigger("Deselected");
        }

        protected virtual void OnAwait() => Animator.SetBool("Await", true);
        protected virtual void OnHidden() => Animator.SetBool("Await", false);

        public override void Subscribe()
        {
            GameState.OnEnter += OnGameStateEnter;
        }
        public override void Unsubscribe()
        {
            GameState.OnEnter -= OnGameStateEnter;
        }

        private void OnGameStateEnter(GameState state)
        {
            if (state is CellSelectionState)
                OnAwait();

            else if (state is MapOverviewState)
                OnHidden();

            else if (state is CellManagementState && !IsSelected)
                OnHidden();
        }
    }
}