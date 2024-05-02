using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyConfig : MonoBehaviour
{
    // ���o�C���h�Ώۂ�Action
    [SerializeField] private InputActionReference _actionRef;

    // ���o�C���h�Ώۂ�Scheme
    [SerializeField] private string _scheme = "Keyboard";

    // ���݂�Binding�̃p�X��\������e�L�X�g
    [SerializeField] private TMP_Text _pathText;

    // ���o�C���h���̃}�X�N�p�I�u�W�F�N�g
    [SerializeField] private GameObject _mask;

    private InputAction _action;
    private InputActionRebindingExtensions.RebindingOperation _rebindOperation;

    // ������
    private void Awake()
    {
        if (_actionRef == null) return;

        // InputAction�C���X�^���X��ێ����Ă���
        _action = _actionRef.action;

        // �L�[�o�C���h�̕\���𔽉f����
        RefreshDisplay();
    }

    // �㏈��
    private void OnDestroy()
    {
        // �I�y���[�V�����͕K���j������K�v������
        CleanUpOperation();
    }

    // ���o�C���h���J�n����
    public void StartRebinding()
    {
        // ����Action���ݒ肳��Ă��Ȃ���΁A�������Ȃ�
        if (_action == null) return;

        // �������o�C���h���Ȃ�A�����I�ɃL�����Z��
        // Cancel���\�b�h�����s����ƁAOnCancel�C�x���g�����΂���
        _rebindOperation?.Cancel();

        // ���o�C���h�O��Action�𖳌�������K�v������
        _action.Disable();

        // ���o�C���h�Ώۂ�BindingIndex���擾
        var bindingIndex = _action.GetBindingIndex(
            InputBinding.MaskByGroup(_scheme)
        );

        // �u���b�L���O�p�}�X�N��\��
        if (_mask != null)
            _mask.SetActive(true);

        // ���o�C���h���I���������̏������s�����[�J���֐�
        void OnFinished()
        {
            // �I�y���[�V�����̌㏈��
            CleanUpOperation();

            // �ꎞ�I�ɖ���������Action��L��������
            _action.Enable();

            // �u���b�L���O�p�}�X�N���\��
            if (_mask != null)
                _mask.SetActive(false);
        }

        // ���o�C���h�̃I�y���[�V�������쐬���A
        // �e��R�[���o�b�N�̐ݒ�����{���A
        // �J�n����
        _rebindOperation = _action
            .PerformInteractiveRebinding(bindingIndex)
            .OnComplete(_ =>
            {
                // ���o�C���h�������������̏���
                RefreshDisplay();
                OnFinished();
            })
            .OnCancel(_ =>
            {
                // ���o�C���h���L�����Z�����ꂽ���̏���
                OnFinished();
            })
            .Start(); // �����Ń��o�C���h���J�n����
    }

    // ���݂̃L�[�o�C���h�\�����X�V
    public void RefreshDisplay()
    {
        if (_action == null || _pathText == null) return;

        _pathText.text = _action.GetBindingDisplayString();
    }

    // ���o�C���h�I�y���[�V������j������
    private void CleanUpOperation()
    {
        // �I�y���[�V�������쐬������ADispose���Ȃ��ƃ��������[�N����
        _rebindOperation?.Dispose();
        _rebindOperation = null;
    }
}