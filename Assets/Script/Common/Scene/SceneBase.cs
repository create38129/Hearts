using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Assets.Script.Common.Scene
{
    /// <summary>
    /// �V�[���x�[�X
    /// </summary>
    public abstract class SceneBase : MonoBehaviour
    {
        /// <summary>
        /// �V�[����
        /// </summary>
        public abstract string SceneName { get; }

        /// <summary>
        /// �V�[���ɕK�v�ȃf�[�^
        /// </summary>
        protected SceneDataBase sceneData;

        /// <summary>
        /// �V�[���f�[�^�̐ݒ�
        /// </summary>
        /// <param name="data"></param>
        public void SetSceneData(SceneDataBase data)
        {
            sceneData = data;
        }

        /// <summary>
        /// ����������
        /// </summary>
        public virtual void OnInitialize()
        {
            // �{�^���o�^�Ƃ�
        }

        /// <summary>
        /// �Ó]������
        /// </summary>
        public virtual UniTask OnAppearPrep()
        {
            // ���[�h�Ƃ�

            return new UniTask();
        }

        /// <summary>
        /// �Ó]������
        /// </summary>
        public virtual void OnViewAppear()
        {
            // ��ʉ��o
        }

        /// <summary>
        /// �Ó]�J�n
        /// </summary>
        public virtual void OnHidePrep()
        {
        }


        /// <summary>
        /// �V�[���j��
        /// </summary>
        public virtual UniTask OnSceneRelease()
        {
            //���\�[�X�����
            return new UniTask();
        }


    }
}