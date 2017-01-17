using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AI_Core
{
    public delegate void AlliedTargetDelagate();
    public delegate void EnemyTargetDelagate();

    #region Allied_AI_Core


    public interface State
    {
        void Update();
    }

    sealed public class Allied_AI_Core : MonoBehaviour
    {
        private State _Currentstate;
        public State CurrentState
        {
            get { return _Currentstate; }
            set { _Currentstate = value; }
        }

        private AlliedTargetDelagate _AlliedTargetDelegate;
        public AlliedTargetDelagate AlliedTargetDelegate
        {
            get { return _AlliedTargetDelegate; }
            set { _AlliedTargetDelegate = value; }
        }

        private MovementState _MovementState;
        public MovementState MovementState
        {
            get { return _MovementState; }
        }

        private AttackState _AttackState;
        public AttackState AttackState
        {
            get { return _AttackState; }
        }


        private void Start()
        {
            _MovementState = new MovementState(this);
            _AttackState = new AttackState(this);
            //TODO: remove later on.
            new AlliedUnit(new AlliedSwordsmanMovement(this), new AlliedSwordsmanAttack(this));
        }
        private void Update()
        {

            if (_Currentstate != null)
            {
                _Currentstate.Update();
            }
        }
    }

    sealed public class Enemy_AI_Core : MonoBehaviour
    {
        private State _Currentstate;
        public State CurrentState
        {
            get { return _Currentstate; }
            set { _Currentstate = value; }
        }

        private EnemyTargetDelagate _EnemyTargetDelegate;
        public EnemyTargetDelagate EnemyTargetDelegate
        {
            get { return _EnemyTargetDelegate; }
            set { _EnemyTargetDelegate = value; }
        }

        private MovementState _MovementState;
        public MovementState MovementState
        {
            get { return _MovementState; }
        }

        private AttackState _AttackState;
        public AttackState AttackState
        {
            get { return _AttackState; }
        }


        public void Start()
        {
            /*
            _MovementState = new MovementState(this);
            _AttackState = new AttackState(this);
            //TODO: remove later on.
            new AlliedUnit(new AlliedSwordsmanMovement(this), new AlliedSwordsmanAttack(this));
            */
        }
        public void Update()
        {

            if (_Currentstate != null)
            {
                _Currentstate.Update();
            }
        }
    }
    sealed public class MovementState : State
    {
        private Allied_AI_Core _Allied_AI_Core;
        private Enemy_AI_Core _Enemy_AI_Core;

        private List<Vector2> _PathList;

        private Vector2 _TargetPosition;


        //movement variables.
        private float _MovementSpeed;
        private float _AttackDistance;

        public MovementState(Allied_AI_Core allied_AI_Core = null, Enemy_AI_Core enemy_AI_Core = null)
        {
            _TargetPosition = Vector2.zero;

            if (_Allied_AI_Core == null)
            {
                _Allied_AI_Core = allied_AI_Core;
            }
            if (_Enemy_AI_Core == null)
            {
                _Enemy_AI_Core = enemy_AI_Core;
            }

        }

        public void Update()
        {
            if (_Allied_AI_Core != null)
            {
                if (Vector2.Distance(_Allied_AI_Core.transform.position, _TargetPosition) < 1 && _PathList != null &&
                    _PathList.Count >= _PathList.IndexOf(_TargetPosition) + 2)
                {
                    _TargetPosition = _PathList[_PathList.IndexOf(_TargetPosition) + 1];
                }

                RaycastHit2D hit = Physics2D.Raycast(_Allied_AI_Core.transform.position, _TargetPosition, _AttackDistance);

                if (hit && hit.transform.gameObject.GetComponent<Enemy_AI_Core>() != null)
                {
                    _Allied_AI_Core.CurrentState = _Allied_AI_Core.AttackState;
                }

                _Allied_AI_Core.transform.position = Vector2.MoveTowards(_Allied_AI_Core.transform.position, _TargetPosition, _MovementSpeed);
            }
            else if (_Enemy_AI_Core != null)
            {
                if (Vector2.Distance(_Enemy_AI_Core.transform.position, _TargetPosition) < 1 && _PathList != null &&
                    _PathList.Count >= _PathList.IndexOf(_TargetPosition) + 2)
                {
                    _TargetPosition = _PathList[_PathList.IndexOf(_TargetPosition) + 1];
                }

                RaycastHit2D hit = Physics2D.Raycast(_Enemy_AI_Core.transform.position, _TargetPosition, _AttackDistance);

                if (hit && hit.transform.gameObject.GetComponent<Allied_AI_Core>() != null)
                {
                    _Enemy_AI_Core.CurrentState = _Enemy_AI_Core.AttackState;
                }

                _Enemy_AI_Core.transform.position = Vector2.MoveTowards(_Enemy_AI_Core.transform.position, _TargetPosition, _MovementSpeed);
            }

        }

        public void SetVariables(float movementSpeed, float attackDistance, List<Vector2> pathList, Sprite sprite, Animator animator)
        {
            _MovementSpeed = movementSpeed;
            _AttackDistance = attackDistance;
            _PathList = pathList;

            _TargetPosition = _PathList.First();
        }
    }

    sealed public class ResourceMovementState : State
    {
        private Allied_AI_Core _Allied_AI_Core;
        private Enemy_AI_Core _Enemy_AI_Core;

        private List<Vector2> _PathList;

        private Vector2 _TargetPosition;

        //movement variables.
        private float _MovementSpeed;

        public ResourceMovementState(Allied_AI_Core allied_AI_Core = null, Enemy_AI_Core enemy_AI_Core = null)
        {
            _TargetPosition = Vector2.zero;

            _Allied_AI_Core = allied_AI_Core;
            _Enemy_AI_Core = enemy_AI_Core;

        }

        public void Update()
        {
            if (_Allied_AI_Core != null)
            {
                if (Vector2.Distance(_Allied_AI_Core.transform.position, _TargetPosition) < 1 && _PathList != null)
                {

                    if (_PathList.Count >= _PathList.IndexOf(_TargetPosition) + 2)
                    {
                        _TargetPosition = _PathList[_PathList.IndexOf(_TargetPosition) + 1];
                    }
                    else
                    {
                        _PathList.Reverse();
                        _TargetPosition = _PathList.First();
                    }

                }
                _Allied_AI_Core.transform.position = Vector2.MoveTowards(_Allied_AI_Core.transform.position, _TargetPosition, _MovementSpeed);
            }
            else if (_Enemy_AI_Core != null)
            {
                if (Vector2.Distance(_Enemy_AI_Core.transform.position, _TargetPosition) < 1 && _PathList != null)
                {

                    if (_PathList.Count >= _PathList.IndexOf(_TargetPosition) + 2)
                    {
                        _TargetPosition = _PathList[_PathList.IndexOf(_TargetPosition) + 1];
                    }
                    else
                    {
                        _PathList.Reverse();
                        _TargetPosition = _PathList.First();
                    }

                }
                _Enemy_AI_Core.transform.position = Vector2.MoveTowards(_Enemy_AI_Core.transform.position, _TargetPosition, _MovementSpeed);
            }
        }

        public void SetVariables(float movementSpeed, List<Vector2> pathList, Sprite sprite, Animator animator)
        {
            _MovementSpeed = movementSpeed;
            _PathList = pathList;

            _TargetPosition = _PathList.First();
        }
    }

    sealed public class AttackState : State
    {

        private Allied_AI_Core _Allied_AI_Core;
        private Enemy_AI_Core _Enemy_AI_Core;

        private float _Timer;
        private float _AttackSpeed;
        private float _Damage;
        private GameObject _Target;

        public AttackState(Allied_AI_Core allied_AI_Core = null, Enemy_AI_Core enemy_AI_Core = null, GameObject target = null)
        {
            _Timer = _AttackSpeed;
            _Target = target;

            if (_Target != null && _Allied_AI_Core != null && allied_AI_Core != null)
            {
                _Target.GetComponent<Allied_AI_Core>().AlliedTargetDelegate += EnemyDead;
            }
            else if (_Target != null && _Enemy_AI_Core != null && enemy_AI_Core != null)
            {
                _Target.GetComponent<Enemy_AI_Core>().EnemyTargetDelegate += EnemyDead;
            }
        }

        public void Update()
        {
            if (_Timer <= 0)
            {
                //TODO: Add attack.
                _Timer = _AttackSpeed;
            }

            _Timer -= Time.deltaTime;
        }

        private void EnemyDead()
        {
            if (_Allied_AI_Core != null)
            {
                _Allied_AI_Core.CurrentState = _Allied_AI_Core.MovementState;
            }
            else if (_Enemy_AI_Core != null)
            {
                _Enemy_AI_Core.CurrentState = _Enemy_AI_Core.MovementState;
            }
        }

        public void SetVariables(float damage, float attackSpeed, Animator animator)
        {
            _AttackSpeed = attackSpeed;
            _Damage = damage;
            _Timer = _AttackSpeed;
        }

        private void EnemyDied()
        {
            _Allied_AI_Core.CurrentState = _Allied_AI_Core.MovementState;
        }
    }

    #endregion

    #region Create Allied Unit
    public enum e_Upgrades
    {
        none,
        efficiency,
        speed,
        attack
    }

    sealed public class AlliedUnit
    {
        public AlliedUnit(Movement movementType, Attack attackType = null, e_Upgrades upgrade1 = e_Upgrades.none, e_Upgrades upgrade2 = e_Upgrades.none)
        {
            //HOWTO: new AlliedUnit(new SwordsmanMovement(), new SwordsmanAttack(), e_Upgrades.attack);

        }
    }
    #endregion

    #region Movement interface and types
    public interface Movement
    {

    }

    sealed public class AlliedSwordsmanMovement : Movement
    {
        public AlliedSwordsmanMovement(Allied_AI_Core allied_AI_Core)
        {
            allied_AI_Core.MovementState.SetVariables(0.5f, 10, Object.FindObjectOfType<AI_Path>().GetAlliedPath(), new Sprite(), new Animator());
            allied_AI_Core.CurrentState = allied_AI_Core.MovementState;

        }
    }

    sealed public class AlliedMusketmanMovement : Movement
    {

    }
    sealed public class AlliedCavelryMovement : Movement
    {

    }

    sealed public class AlliedCannonMovement : Movement
    {
    }

    sealed public class AlliedResourceMovement : Movement
    {
    }
    #endregion

    #region Attack interface and types
    public interface Attack
    {

    }
    sealed public class AlliedSwordsmanAttack : Attack
    {
        public AlliedSwordsmanAttack(Allied_AI_Core allied_AI_Core)
        {
            //TODO: Add animator and fix it.
            allied_AI_Core.AttackState.SetVariables(10,10, new Animator());
        }
    }

    sealed public class AlliedMusketmanAttack : Attack
    {

    }
    sealed public class AlliedCavelryAttack : Attack
    {

    }

    sealed public class AlliedCannonAttack : Attack
    {

    }

    sealed public class AlliedResourceAttack : Attack
    {

    }
    #endregion
}
