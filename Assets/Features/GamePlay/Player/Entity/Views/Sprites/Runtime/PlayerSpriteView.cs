﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.Sprites.Logs;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Views.Sprites.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerSpriteView :
        MonoBehaviour,
        IPlayerAwakeListener,
        ISpriteSwitcher,
        ISpriteMaterial,
        ISpriteFlipper,
        ISpriteView,
        ISpriteFlash
    {
        [Inject]
        public void Construct(ILogger logger)
        {
            _logger = new SpriteViewLogger(logger, _logSettings);
        }

        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _flash;

        [SerializeField] private SpriteViewLogSettings _logSettings;
        [SerializeField] private List<SpriteRenderer> _subSprites;
        [SerializeField] private GameObject _scaleFlipped;

        private SpriteViewLogger _logger;

        private SpriteRenderer _sprite;

        public void OnAwake()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void Flash(float time)
        {
            _sprite.material = _flash;

            UniTask.Create(async () =>
            {
                var delay = (int)(time * 1000f);

                await UniTask.Delay(
                    delay, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());

                _sprite.material = _defaultMaterial;
            });
        }

        public void ResetRotation()
        {
            _sprite.flipX = false;

            foreach (var subSprite in _subSprites)
                subSprite.flipX = false;
            if (_scaleFlipped)
                _scaleFlipped.transform.localScale = Vector3.one;
        }

        public void SetFlipX(bool isFlipped, bool flipSubSprites)
        {
            _sprite.flipX = isFlipped;
            if (_scaleFlipped)
                _scaleFlipped.transform.localScale = isFlipped ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

            if (flipSubSprites == true)
                foreach (var subSprite in _subSprites)
                    subSprite.flipX = _sprite.flipX;

            _logger.OnFlipSetted(isFlipped);
        }

        public void FlipAlong(Vector2 direction, bool flipSubSprites)
        {
            _sprite.flipX = direction.x switch
            {
                > 0f => false,
                < 0f => true,
                _ => _sprite.flipX
            };
            if (_scaleFlipped)
                _scaleFlipped.transform.localScale = _sprite.flipX ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

            if (flipSubSprites == true)
                foreach (var subSprite in _subSprites)
                    subSprite.flipX = _sprite.flipX;

            _logger.OnFlippedAlong(direction);
        }

        public Material Material
        {
            get
            {
                _logger.OnMaterialUsed(_sprite.material);
                return _sprite.material;
            }
        }

        public void SetMaterial(Material material)
        {
            _sprite.material = material;

            _logger.OnMaterialSetted(material);
        }

        public void Enable(bool enableSubSprites = false)
        {
            _sprite.enabled = true;

            if (enableSubSprites == true)
                foreach (var subSprite in _subSprites)
                    subSprite.enabled = true;

            _logger.OnEnabled();
        }

        public void Disable(bool disableSubSprites = false)
        {
            _sprite.enabled = false;

            if (disableSubSprites == true)
                foreach (var subSprite in _subSprites)
                    subSprite.enabled = false;

            _logger.OnDisabled();
        }

        public void AddSubSprite(SpriteRenderer subSprite)
        {
            _subSprites.Add(subSprite);

            subSprite.flipX = _sprite.flipX;
        }

        public void RemoveSubSprite(SpriteRenderer subSprite)
        {
            _subSprites.Remove(subSprite);
        }
    }
}