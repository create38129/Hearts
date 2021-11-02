using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Script.Common.Scene
{
    /// <summary>
    /// �V�[���x�[�X
    /// </summary>
    public abstract class SceneModelBase
    {
        /// <summary>
        /// �V�[����
        /// </summary>
        public abstract string SceneName { get; }

        public abstract string ScenePrefab { get; }

        /// <summary>
        /// �V�[�����A�N�e�B�u��Ԃ�
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsActive => isActive.ToReadOnlyReactiveProperty();
        private readonly ReactiveProperty<bool> isActive = new ReactiveProperty<bool>(false);

        /// <summary>
        /// �V�[�����������鎞�ɒʒm
        /// </summary>
        public IObservable<Unit> OnRelease => onRelease;
		private readonly Subject<Unit> onRelease = new Subject<Unit>();


        /// <summary>
        /// ����������
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// �Ó]������
        /// </summary>
        public virtual UniTask AppearPrep()
        {
            // ���[�h�Ƃ�

            return new UniTask();
        }

        /// <summary>
        /// �Ó]������
        /// </summary>
        public virtual void ViewAppear()
        {
            // ��ʉ��o
        }

        /// <summary>
        /// �Ó]�J�n
        /// </summary>
        public virtual void HidePrep()
        {
        }


        /// <summary>
        /// �V�[���j��
        /// </summary>
        public virtual UniTask SceneRelease()
        {
            onRelease.OnNext(Unit.Default);

            //���\�[�X�����
            return new UniTask();
        }

        public void SetActive(bool isActive)
        {
            this.isActive.Value = isActive;
        }
    }
}