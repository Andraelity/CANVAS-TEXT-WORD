using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using readDataFile;

public class PlaneSquare : MonoBehaviour
{
    // Start is called before the first frame update


    private float timeNow = 0;
    private int HEIGHT = 1000;
    private int WIDTH  = 2000;

    private int HEIGHTlargeMemory = 3000;
    private int WIDTHlargeMemory  = 6000;


    private int _Xaxis22size = 1900;
    private int _Yaxis22size = 480;

    Renderer render;
    Material mat;


    public ComputeShader computacion;

    static int _kernelforLoadImageColorPlane;

    static int _kernelforLoadTextureColorPosition;
    static int _kernelforPrintTextureColor;
    static int _kernelforPrintTextureEraserColor;


    static int _kernelforLoadTextureBlanckPosition;
    static int _kernelforPrintTextureBlanck;
    
    static int _kernelforLoadPointerPosition;

    static int _kernelforPrintPointer;

    static int _kernelforPrintPointerBlack;
    
    
    ComputeBuffer bufferImageColorPlane;
    
    ComputeBuffer bufferLargeMemory;

    ComputeBuffer bufferTextureColorPositionX;
    ComputeBuffer bufferTextureColorPositionY;

    ComputeBuffer bufferTextureBlanckPositionX;
    ComputeBuffer bufferTextureBlanckPositionY;


    ComputeBuffer bufferPointerPositionX;
    ComputeBuffer bufferPointerPositionY;

    ComputeBuffer bufferColorPointer;


    int receiveSize = 0;
    int manageSize = 0;
    int PositionPointerX = 500;
    int PositionPointerY = 500;

    int countTextField = 0; 

    public GameObject cursor;

    public ParticleSystem particle_RedStar0;
    public ParticleSystem particle_RedStar1;

    static public List<List<int>> sizeWords_OneLanguage = new List<List<int>>();
    static public List<List<int>> sizeWords_TwoLanguage = new List<List<int>>();

    static public int readPosition_communication_OneLanguage = 0;
    static public int readPosition_communication_TwoLanguage = 0;

    static public List<int> tuplePosition_OneLanguage = new List<int>(){0, 0};
    static public List<int> tuplePosition_TwoLanguage = new List<int>(){0, 0};

    bool flagPaintHighlight_OneLanguage = false;
    bool flagPaintHighlight_TwoLanguage = false;


    void Start()
    {

        render = GetComponent<Renderer>();
        mat = render.material;

        computacion.SetInt("_WIDTH", WIDTH);
        computacion.SetInt("_WIDTHlargeMemory", WIDTHlargeMemory);

        mat.SetInt("_WIDTH", WIDTH);
        mat.SetInt("_HEIGHT", HEIGHT);

        bufferLargeMemory = new ComputeBuffer(HEIGHTlargeMemory * WIDTHlargeMemory, 16);
        bufferImageColorPlane = new ComputeBuffer(HEIGHT * WIDTH, 16);

        mat.SetBuffer("bufferImageColorPlane", bufferImageColorPlane);


        _kernelforLoadImageColorPlane = computacion.FindKernel("forLoadImageColorPlane");

        _kernelforLoadTextureColorPosition = computacion.FindKernel("forLoadTextureColorPosition");
        _kernelforPrintTextureColor = computacion.FindKernel("forPrintTextureColor");
        _kernelforPrintTextureEraserColor = computacion.FindKernel("forPrintTextureEraserColor");

        _kernelforLoadTextureBlanckPosition = computacion.FindKernel("forLoadTextureBlanckPosition");
        _kernelforPrintTextureBlanck = computacion.FindKernel("forPrintTextureBlanck");


        bufferTextureColorPositionX = new ComputeBuffer(136, 4);
        bufferTextureColorPositionY = new ComputeBuffer(136, 4);

        bufferTextureBlanckPositionX = new ComputeBuffer(10000, 4);
        bufferTextureBlanckPositionY = new ComputeBuffer(10000, 4);


        bufferPointerPositionX = new ComputeBuffer(440, 4);
        bufferPointerPositionY = new ComputeBuffer(440, 4);

        bufferColorPointer = new ComputeBuffer(3, 4);


        computacion.SetBuffer(_kernelforLoadImageColorPlane, "bufferImageColorPlane", bufferImageColorPlane);
        computacion.SetBuffer(_kernelforLoadImageColorPlane, "bufferLargeMemory", bufferLargeMemory);

        computacion.SetBuffer(_kernelforLoadTextureColorPosition, "bufferTextureColorPositionX", bufferTextureColorPositionX);
        computacion.SetBuffer(_kernelforLoadTextureColorPosition, "bufferTextureColorPositionY", bufferTextureColorPositionY);

        computacion.SetBuffer(_kernelforPrintTextureColor,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTextureColor,"bufferTextureColorPositionX", bufferTextureColorPositionX);
        computacion.SetBuffer(_kernelforPrintTextureColor,"bufferTextureColorPositionY", bufferTextureColorPositionY);

        computacion.SetBuffer(_kernelforPrintTextureEraserColor,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTextureEraserColor,"bufferTextureColorPositionX", bufferTextureColorPositionX);
        computacion.SetBuffer(_kernelforPrintTextureEraserColor,"bufferTextureColorPositionY", bufferTextureColorPositionY);



        computacion.SetBuffer(_kernelforLoadTextureBlanckPosition, "bufferTextureBlanckPositionX", bufferTextureBlanckPositionX);
        computacion.SetBuffer(_kernelforLoadTextureBlanckPosition, "bufferTextureBlanckPositionY", bufferTextureBlanckPositionY);

        computacion.SetBuffer(_kernelforPrintTextureBlanck,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTextureBlanck,"bufferTextureBlanckPositionX", bufferTextureBlanckPositionX);
        computacion.SetBuffer(_kernelforPrintTextureBlanck,"bufferTextureBlanckPositionY", bufferTextureBlanckPositionY);


        
        loadTextureColorPosition();

        
    }


    int elementoCount = 0;

    // Update is called once per frame
    void Update()
    {


        timeNow = Time.realtimeSinceStartup;


        if(Input.GetKeyUp(KeyCode.F10))
        {
            
            loadTextureBlanckPosition();
            printTextureBlanck();

            Debug.Log("Here in space");
        }



        if(Input.GetKeyUp(KeyCode.F7))
        {

            sizeWords_OneLanguage = ScriptCommunication.sizeOfWords_communication_OneLanguage;
            sizeWords_TwoLanguage = ScriptCommunication.sizeOfWords_communication_TwoLanguage;

            tuplePosition_OneLanguage = positionConversion(elementoCount, sizeWords_OneLanguage);
            // tuplePosition_OneLanguage = new List<int>(){0,1};

            paintWordHighlight_OneLanguage();

            tuplePosition_TwoLanguage = positionConversion(elementoCount, sizeWords_TwoLanguage);

            paintWordHighlight_TwoLanguage();
            

            elementoCount ++ ;

        }       



        if(Input.GetKeyUp(KeyCode.F6))       
        {
            unpaintWordHighlight_OneLanguage();
            unpaintWordHighlight_TwoLanguage();

        }
        
        if(Input.GetKeyUp(KeyCode.F5))       
        {

            paintWordHighlight_OneLanguage();

            paintWordHighlight_TwoLanguage();

        } 

        // if(Input.GetKeyUp(KeyCode.F6))       
        // {

        //     unpaintWordHighlight_OneLanguage();

        //     unpaintWordHighlight_TwoLanguage();

        // } 

        if(ScriptCommunication.flagPaintHighlight_OneLanguage == true)
        {

            sizeWords_OneLanguage = ScriptCommunication.sizeOfWords_communication_OneLanguage;


            readPosition_communication_OneLanguage = ScriptCommunication.positionInt_OneLanguage;

            if(readPosition_communication_OneLanguage == 0)
            {
                for(int i = 0; i < 40 ; ++i)
                {
                    printTextureEraserColor(1900 - (i * 17), 130);        
                }
            }


            tuplePosition_OneLanguage = positionConversion(readPosition_communication_OneLanguage, sizeWords_OneLanguage);


            paintWordHighlight_OneLanguage();

            ScriptCommunication.flagPaintHighlight_OneLanguage = false;

        }



        if(ScriptCommunication.flagUnPaintHighlight_OneLanguage == true)
        {

            unpaintWordHighlight_OneLanguage();
            ScriptCommunication.flagUnPaintHighlight_OneLanguage = false;   

        }



        if(ScriptCommunication.flagPaintHighlight_TwoLanguage == true)
        {

            sizeWords_TwoLanguage = ScriptCommunication.sizeOfWords_communication_TwoLanguage;

            readPosition_communication_TwoLanguage = ScriptCommunication.positionInt_TwoLanguage;


            tuplePosition_TwoLanguage = positionConversion(readPosition_communication_TwoLanguage, sizeWords_TwoLanguage);


            paintWordHighlight_TwoLanguage();

            ScriptCommunication.flagPaintHighlight_TwoLanguage = false;

        }



        if(ScriptCommunication.flagUnPaintHighlight_TwoLanguage == true)
        {

            unpaintWordHighlight_TwoLanguage();
            ScriptCommunication.flagUnPaintHighlight_TwoLanguage = false;   

        }



        if(Input.GetKeyUp(KeyCode.F8))
        {

            loadTextureColorPosition();
            
            printTextureColor(1900,130);

        }



        if(Input.GetKeyUp(KeyCode.F8))
        {
            
            Vector3 variableLearn = cursor.transform.position;

            cursor.transform.position = new Vector3(variableLearn.x + 0.1f,variableLearn.y, variableLearn.z);

        }



        if(ScriptCommunication.activatePrefabRedStar == true)
        {
            
            // Vector3 positionInScreen = new Vector3(0, 1.5f, 3.78f);

            // Instantiate(Resources.Load("prefab/particle_Electrification"), positionInScreen, Quaternion.identity);
            particle_RedStar0.Play(true);
            particle_RedStar1.Play(true);

            ScriptCommunication.activatePrefabRedStar = false;

        }



        if(Input.GetKeyUp(KeyCode.F9))
        {

            particle_RedStar0.Play(true);
            particle_RedStar1.Play(true);

            // particle_Electrification_management = Instantiate(particle_Electrification_management, particle_Electrification_management.transform.position, particle_Electrification_management.transform.rotation);
            // particle_Electrification_management = Instantiate(particle_Electrification_management, particle_Electrification_management.transform.position, particle_Electrification_management.transform.rotation);
            // Instantiate(Resources.Load<GameObject>("prefab/particle_Electrification"), positionInScreen, Quaternion.identity);

        }   
    

        loadImageColorPlane();


    }



    void OnDestroy()
    {

        bufferImageColorPlane.Release();
        bufferImageColorPlane.Dispose();
        bufferImageColorPlane = null;

        bufferLargeMemory.Release();
        bufferLargeMemory.Dispose();
        bufferLargeMemory = null;

        bufferTextureColorPositionX.Release();
        bufferTextureColorPositionX.Dispose();
        bufferTextureColorPositionX = null;

        bufferTextureColorPositionY.Release();
        bufferTextureColorPositionY.Dispose();
        bufferTextureColorPositionY = null;

        bufferTextureBlanckPositionX.Release();
        bufferTextureBlanckPositionX.Dispose();
        bufferTextureBlanckPositionX = null;

        bufferTextureBlanckPositionY.Release();
        bufferTextureBlanckPositionY.Dispose();
        bufferTextureBlanckPositionY = null;

    }




    public List<int> positionConversion(int position, List<List<int>> words_sizes)
    {

        //Input
        int container_position = position;

        List<List<int>> container_words_sizes = words_sizes;


        // Output
        List<int> containerOutput = new List<int>(){0, 0};


        // Algorithm 

        int count = 0;

        bool flag = false;


        for(int i = 0; i < container_words_sizes.Count; ++i)
        {


            
            for(int j = 0; j < container_words_sizes[i].Count; ++j)
            {  

                if(count == container_position)
                {
                    flag = true;

                    containerOutput[0] = i;
                    containerOutput[1] = j;

                    break;
                }

                count ++;

            }

            if(flag == true)
            {
                break;
            }

        }


        return containerOutput;

    }





    public void paintWordHighlight_OneLanguage()
    {

        sizeWords_OneLanguage = ScriptCommunication.sizeOfWords_communication_OneLanguage;

        // List<int> position_OneLanguage = ScriptCommunication.position_OneLanguage;

        List<int> position_OneLanguage = tuplePosition_OneLanguage;

        // Debug.Log(sizeWords_OneLanguage.Count);

        // for(int i = 0 ; i < sizeWords_OneLanguage.Count; ++i)
        // {   
        //     string readWordSize = "";
        //     for(int j = 0; j < sizeWords_OneLanguage[i].Count; ++j)
        //     {
        //         readWordSize += sizeWords_OneLanguage[i][j].ToString() + " ";
        //     }
        //     Debug.Log(readWordSize);
        // }
        // adding word sizes.

        int PositionY_OneLanguage = position_OneLanguage[0] * 50 + 130;

        List<int> add_wordSizes = sizeWords_OneLanguage[position_OneLanguage[0]];

        int increment_ratio = 0;

        for(int i = 0; i < position_OneLanguage[1]; ++i)
        {
            increment_ratio += add_wordSizes[i] + 1;
        }

        int PositionX_OneLanguage = increment_ratio * 17;

        for(int i = 0; i < add_wordSizes[position_OneLanguage[1]]; ++i)
        {

            printTextureColor(1900 - (PositionX_OneLanguage + (i * 17)),PositionY_OneLanguage);

        }

    }


    public void unpaintWordHighlight_OneLanguage()
    {

        sizeWords_OneLanguage = ScriptCommunication.sizeOfWords_communication_OneLanguage;

        // List<int> position_OneLanguage = ScriptCommunication.position_OneLanguage;
        List<int> position_OneLanguage = tuplePosition_OneLanguage;
        
        // adding word sizes.
        int PositionY_OneLanguage = position_OneLanguage[0] * 50 + 130;

        List<int> add_wordSizes = sizeWords_OneLanguage[position_OneLanguage[0]];

        int increment_ratio = 0;

        for(int i = 0; i < position_OneLanguage[1]; ++i)
        {
            increment_ratio += add_wordSizes[i] + 1;
        }

        int PositionX_OneLanguage = increment_ratio * 17;

        for(int i = 0; i < add_wordSizes[position_OneLanguage[1]]; ++i)
        {    
            printTextureEraserColor(1900 - (PositionX_OneLanguage + (i * 17)),PositionY_OneLanguage);
        }

    }


    public void paintWordHighlight_TwoLanguage()
    {

        sizeWords_OneLanguage = ScriptCommunication.sizeOfWords_communication_OneLanguage;

        sizeWords_TwoLanguage = ScriptCommunication.sizeOfWords_communication_TwoLanguage;

        // List<int> position_OneLanguage = ScriptCommunication.position_OneLanguage;

        List<int> position_TwoLanguage = tuplePosition_TwoLanguage;


        // Debug.Log(sizeWords_OneLanguage.Count);
        // for(int i = 0 ; i < sizeWords_OneLanguage.Count; ++i)
        // {   
        //     string readWordSize = "";
        //     for(int j = 0; j < sizeWords_OneLanguage[i].Count; ++j)
        //     {
        //         readWordSize += sizeWords_OneLanguage[i][j].ToString() + " ";
        //     }
        //     Debug.Log(readWordSize);
        // }
        // adding word sizes.


        int PositionY_TwoLanguage = position_TwoLanguage[0] * 50 + 630;

        List<int> add_wordSizes = sizeWords_TwoLanguage[position_TwoLanguage[0]];

        int increment_ratio = 0;

        for(int i = 0; i < position_TwoLanguage[1]; ++i)
        {
            increment_ratio += add_wordSizes[i] + 1;
        }

        int PositionX_TwoLanguage = increment_ratio * 17;

        for(int i = 0; i < add_wordSizes[position_TwoLanguage[1]]; ++i)
        {

            printTextureColor(1900 - (PositionX_TwoLanguage + (i * 17)),PositionY_TwoLanguage);

        }

    }


    public void unpaintWordHighlight_TwoLanguage()
    {

        sizeWords_OneLanguage = ScriptCommunication.sizeOfWords_communication_OneLanguage;

        sizeWords_TwoLanguage = ScriptCommunication.sizeOfWords_communication_TwoLanguage;

        // List<int> position_OneLanguage = ScriptCommunication.position_OneLanguage;
        List<int> position_TwoLanguage = tuplePosition_TwoLanguage;
        
        // adding word sizes.
        int PositionY_TwoLanguage = position_TwoLanguage[0] * 50 + 630;

        List<int> add_wordSizes = sizeWords_TwoLanguage[position_TwoLanguage[0]];

        int increment_ratio = 0;

        for(int i = 0; i < position_TwoLanguage[1]; ++i)
        {
            increment_ratio += add_wordSizes[i] + 1;
        }

        int PositionX_TwoLanguage = increment_ratio * 17;

        for(int i = 0; i < add_wordSizes[position_TwoLanguage[1]]; ++i)
        {    
            printTextureEraserColor(1900 - (PositionX_TwoLanguage + (i * 17)), PositionY_TwoLanguage);
        }

    }




    public void loadImageColorPlane()
    {
     
        computacion.Dispatch(_kernelforLoadImageColorPlane, 2000, 1000, 1);
    
    }    


    public void loadTextureColorPosition()
    {

        computacion.Dispatch(_kernelforLoadTextureColorPosition, 8, 1, 1);

    }


    public void printTextureColor(int Xaxis, int Yaxis)
    {

        int _PositionTextureColorX = Xaxis;
        int _PositionTextureColorY = Yaxis;

        computacion.SetInt( "_PositionTextureColorX", _PositionTextureColorX);
        computacion.SetInt( "_PositionTextureColorY", _PositionTextureColorY);

        computacion.Dispatch(_kernelforPrintTextureColor, 8, 1, 1);

    }


    public void printTextureEraserColor(int Xaxis, int Yaxis)
    {

        int _PositionTextureEraserColorX = Xaxis;
        int _PositionTextureEraserColorY = Yaxis;

        computacion.SetInt( "_PositionTextureEraserColorX", _PositionTextureEraserColorX);
        computacion.SetInt( "_PositionTextureEraserColorY", _PositionTextureEraserColorY);

        computacion.Dispatch(_kernelforPrintTextureEraserColor, 8, 1, 1);

    }


    public void loadTextureBlanckPosition()
    {

        computacion.Dispatch(_kernelforLoadTextureBlanckPosition, 100, 1, 1);

    }


    public void printTextureBlanck()
    {


        int _PositionTextureBlanckX = 100;
        int _PositionTextureBlanckY = 100;

        computacion.SetInt( "_PositionTextureBlanckX", _PositionTextureBlanckX);
        computacion.SetInt( "_PositionTextureBlanckY", _PositionTextureBlanckY);

        computacion.Dispatch(_kernelforPrintTextureBlanck, 100, 1, 1);

    }


}
