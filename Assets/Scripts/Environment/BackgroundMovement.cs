using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BackgroundMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 parallaxEffectMultiplier;
    [SerializeField] bool infiniteHorizontal = false;
    [SerializeField] bool infiniteVertical = false;
    public static BackgroundMovement instance;
    public Transform activeCam;

    public Transform cameraTransform;
    private Vector3 lastCameraPosition;
    float textureUnitSizeX;
    float textureUnitSizeY;
    private void Start()
    {
        instance = this;
        //cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {   cameraTransform = activeCam.transform;
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;
        if (infiniteHorizontal)
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        if (infiniteVertical)
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY, transform.position.y);
            }

    }
}
