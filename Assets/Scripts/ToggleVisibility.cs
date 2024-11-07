using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ToggleVisibility : MonoBehaviour {
    [SerializeField] public GameObject object1;
    [SerializeField] public GameObject object2;

    private bool isWorld1Active = true;

    private bool onLight = true;

    public Transform player;

    private Tilemap world1;
    private Tilemap world2;

    private Tilemap tilemap;

    private TileBase currentTile;
    private Vector3Int lastTilePosition;

    public GameObject tilePrefab;
    private GameObject newTileObject;

    void Start() {
        world1 = object1.GetComponent<Tilemap>();
        world2 = object2.GetComponent<Tilemap>();

        tilemap = world1;
        tilemap = FindObjectOfType<Tilemap>();

        lastTilePosition = Vector3Int.zero;

        object1.SetActive(true);
        object2.SetActive(false);
    }
    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            if (onLight)
                SwitchWorld();
        }

        if (Input.GetMouseButtonDown(1)) {
            onLight = !onLight;
        }
    }

    void SwitchWorld() {
        if (newTileObject)
            Destroy(newTileObject);

        isWorld1Active = !isWorld1Active;
            
        if (isWorld1Active) {
            tilemap = world1;
            tilemap = FindObjectOfType<Tilemap>();
        } else {
            tilemap = world2;
            tilemap = FindObjectOfType<Tilemap>();
        }

        Vector3Int playerPosition;
        playerPosition = tilemap.WorldToCell(player.position);
        currentTile = tilemap.GetTile(playerPosition);

        object1.SetActive(isWorld1Active);
        object2.SetActive(!isWorld1Active);

        TileBase tileDown;
        if (tilemap == world1) {
            tileDown = world2.GetTile(playerPosition);
        }
        else
        {
            tileDown = world1.GetTile(playerPosition);
        }

        if (!tileDown) {
            if (currentTile != null) {
                newTileObject = Instantiate(tilePrefab, new Vector3((float)playerPosition.x + 0.5f, (float)playerPosition.y + 0.5f, 0), Quaternion.identity);
                SpriteRenderer spriteRenderer = newTileObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null) {
                    if (currentTile is Tile tile) {
                        spriteRenderer.sprite = tile.sprite;
                    }
                }
            }
        }

    }
}
