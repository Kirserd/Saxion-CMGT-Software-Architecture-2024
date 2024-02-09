using MIRAI.Grid.Cell;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MIRAI.Grid
{
    public class GridInteractor : MonoBehaviour
    {
        public delegate void OnSelected(GridCellShell shell);
        public static OnSelected OnSelectedHandler;

        public static GridCellShell Selection { get; private set; }

        private void Awake()
        {
            Controls.Subscribe(OnMouseAction, 0);
            Controls.Subscribe(OnMouseAction, 1);
        }

        private void OnMouseAction(InputCallbackData data)
        {
            if (!data.IsPressed || EventSystem.current.IsPointerOverGameObject())
                return;

            SelectionBroker(Physics2D.Raycast(GetMousePositionWorld(), Vector2.zero));
        }
        private Vector3 GetMousePositionWorld()
        {
            Vector3 mousePositionScreen = Input.mousePosition;

            return Camera.main.ScreenToWorldPoint(
                    new Vector3
                    (
                        mousePositionScreen.x,
                        mousePositionScreen.y,
                        Camera.main.nearClipPlane)
                    );
        }

        private void SelectionBroker(RaycastHit2D hit)
        {
            if (GameStateMachine.CurrentIs<MapOverviewState>())
            {
                StartSelection(); // Add reasonable check >:c
                return;
            }

            if (GameStateMachine.CurrentIs<CellManagementState>())
                ReturnFromManagement();

            if (GameStateMachine.CurrentIs<CellSelectionState>())
                TrySelect(hit);
        }

        //Map Overview State
        private void StartSelection() => GameStateMachine.Next<CellSelectionState>();
        public void ReturnFromManagement()
        {
            GameStateMachine.Next<MapOverviewState>();
        }

        //Cell Selection State
        private void TrySelect(RaycastHit2D hit)
        {
            if (hit.collider == null)
            {
                GameStateMachine.Return();
                return;
            }

            hit.collider.gameObject.TryGetComponent(out GridCellShell shell);

            if (shell == null)
            {
                GameStateMachine.Return();
                return;
            }

            Select(shell);
            GameStateMachine.Next<CellManagementState>();
        }
        private static void Select(GridCellShell shell)
        {
            Selection = shell;
            OnSelectedHandler?.Invoke(shell);
        }
        public static void Deselect() => Select(null);
    }
}