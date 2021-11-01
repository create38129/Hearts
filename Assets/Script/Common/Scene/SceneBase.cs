using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


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
        /// �Ó]������
        /// </summary>
        public virtual void OnAppearPrep()
        {
            // ���[�h�Ƃ�
        }

        /// <summary>
        /// �Ó]����
        /// </summary>
        public virtual void OnViewAppear()
        {

        }

        /// <summary>
        /// �Ó]�J�n
        /// </summary>
        public virtual void OnHide()
        {
        }


        /// <summary>
        /// �V�[���j��
        /// </summary>
        public virtual void OnSceneRelease()
        {
        }


    }
}