 #pragma strict 
 
// Object Script
var speed : float = 10.0;
var shiftPressed : float = 20.0;
private var tempShift : float = 1.0;
private var cameraRotation : Vector3;
private var move : Vector3;


@Tooltip("Is camera following any object? if False can't use WASD")
public var followCenter: boolean;

@Tooltip("Control Scroll Speed")
@Range(0f,20f)
var scrollSpeed: float = 5.0; 
@Space (10) //Attribute
var heightMainStart: float = 15.0; 
var heightMainUpdate: float = 0.0; 
private var heightMainBreak: boolean;
//var heightSecondStart: float = 10.0; 
var heightSecondUpdate: float = 0.0; 
private var heightSecondBreak: boolean;
@Space (5)
var heightMin: float = 5.0; 
var heightMax: float = 15.0; 
@Space (20)
var mainCamera : Camera;
var secondCamera : Camera;
@Space (10)
var mainCameraTransform : Transform;
@Space (10)
var targetObjectTransform : Transform;


private var targetObjectTransformVector : Vector3;
private var defaultCameraPosition : Vector3;
private var defaultCameraRotation : Vector3;
private var updatedCamera : Vector3;
@Space (10)
public var moveToLocation1 : Transform;

// public var moveToLocation2 : Transform;
// public var moveToLocation3 : Transform;
// public var moveToLocation4 : Transform;
// public var moveToLocation5 : Transform;
// public var moveToLocation6 : Transform;
// public var moveToLocation7 : Transform;
// public var moveToLocation8 : Transform;
// public var moveToLocation9 : Transform;
// public var moveToLocation0 : Transform;

// var defaultMaterial : Shader;
	
function Start() {

	mainCamera.enabled = true;
	secondCamera.enabled = false;
	mainCameraTransform.localPosition = Vector3(0,0,0);

	mainCamera.orthographicSize = heightMainStart;
	secondCamera.fieldOfView = heightMainStart;	

	targetObjectTransformVector = targetObjectTransform.position;
	//if(mainCamera.enabled == true){
		defaultCameraPosition = mainCameraTransform.transform.position;
		defaultCameraRotation = mainCameraTransform.transform.eulerAngles;

	 	mainCameraTransform.eulerAngles.y = defaultCameraRotation.y;
	 	tempShift = speed;

	 	//Debug.Log(mainCameraTransform.eulerAngles.y);
	//}

} 

function Update() {


 	if(Input.GetKeyDown(KeyCode.Tab) && mainCamera.enabled == true){
 		//Debug.Log('Main Camera Enabled');
		mainCamera.enabled = false;
		secondCamera.enabled = true;


 	}else if(Input.GetKeyDown(KeyCode.Tab) && mainCamera.enabled == false){
 		//Debug.Log('Main Camera Disabled');
		mainCamera.enabled = true;
		secondCamera.enabled = false;
 	}

 	//mainCamera.orthographicSize += Mathf.Round((Input.GetAxis("Mouse ScrollWheel") * scrollSpeed) * 10)/10;



 	mainCamera.orthographicSize += Mathf.Round(-Input.GetAxis("Mouse ScrollWheel") * 10)/10 * scrollSpeed;
 	mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, heightMin, heightMax);

 	secondCamera.fieldOfView += Mathf.Round(-Input.GetAxis("Mouse ScrollWheel") * 10)/15 * scrollSpeed;
 	secondCamera.fieldOfView = Mathf.Clamp(secondCamera.fieldOfView, heightMin, heightMax);


 	heightMainUpdate = mainCamera.orthographicSize ;
 	heightSecondUpdate = secondCamera.fieldOfView ;



	 	// if(Input.GetKeyDown(KeyCode.Q)){
	 	// 	mainCamera.SetReplacementShader(defaultMaterial, "RenderType");
	 	// }


	 	cameraRotation.y = mainCameraTransform.eulerAngles.y;

	 	if(Input.GetKeyDown(KeyCode.LeftShift)){
	 		speed = shiftPressed;
	 	}else if(Input.GetKeyUp(KeyCode.LeftShift)){
	 		speed = tempShift;
	 	}

	 	if(followCenter == true){ // Is camera following any object?

		 	if(cameraRotation.y >= 0 && cameraRotation.y <= 89){
				move = Vector3(-Input.GetAxis("Horizontal"), 0,-Input.GetAxis("Vertical"));
		 	}else if(cameraRotation.y >= 90 && cameraRotation.y <= 179){
		 		move = Vector3(-Input.GetAxis("Vertical"), 0,Input.GetAxis("Horizontal"));
		 	}else if(cameraRotation.y >= 180 && cameraRotation.y <= 269){
		 		move = Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));
		 	}else if(cameraRotation.y >= 270 && cameraRotation.y <= 360){
		 		move = Vector3(Input.GetAxis("Vertical"), 0,-Input.GetAxis("Horizontal"));
		 	}


		 	if (Input.GetKeyDown(KeyCode.Space)){ // reset position
		 		//Debug.Log('Pressed');

		 		//targetObjectTransform.position = Vector3(0,0,0);
		 		targetObjectTransform.position = targetObjectTransformVector;
			 	mainCameraTransform.eulerAngles =  Vector3(0,0,0);

		 	}

		 	if (Input.GetKeyDown(KeyCode.Alpha1)){ // reset position
		 		targetObjectTransform.position = moveToLocation1.position;
			 	// mainCameraTransform.eulerAngles =  Vector3(0,0,0);
		 	}


	 	}
	    targetObjectTransform.position += move * speed * Time.deltaTime ;

		mainCameraTransform.position = targetObjectTransform.position + defaultCameraPosition;

		updatedCamera.y = mainCameraTransform.eulerAngles.y; 




	///////////////////////


	 	if(mainCameraTransform.eulerAngles.y == 0 && cameraRotation.y <= 89){ // Value 0

		 	if (Input.GetKeyDown(KeyCode.LeftBracket)){ // Negatve 90
		 		mainCameraTransform.eulerAngles = Vector3(0,270,0);
		 		//Debug.Log(mainCameraTransform.eulerAngles.y);
		 	}

		 	if (Input.GetKeyDown(KeyCode.RightBracket)){ // Positive 90
		 		mainCameraTransform.eulerAngles = Vector3(0,90,0);
		  		//Debug.Log(mainCameraTransform.eulerAngles.y);
		 	}

	 	}else if(mainCameraTransform.eulerAngles.y == 90 && cameraRotation.y <= 179){ // Value 90

		 	if (Input.GetKeyDown(KeyCode.LeftBracket)){ 
		 		mainCameraTransform.eulerAngles = Vector3(0,0,0);
		 		//Debug.Log(mainCameraTransform.eulerAngles.y);
		 	}

		 	if (Input.GetKeyDown(KeyCode.RightBracket)){ 
		 		mainCameraTransform.eulerAngles = Vector3(0,180,0);
		  		//Debug.Log(mainCameraTransform.eulerAngles.y);
		 	}

	 	}else if(mainCameraTransform.eulerAngles.y >= 180 && mainCameraTransform.eulerAngles.y <= 269){ // Value 180

		 	if (Input.GetKeyDown(KeyCode.LeftBracket)){ 
		 		mainCameraTransform.eulerAngles = Vector3(0,90,0);
		 		//Debug.Log(mainCameraTransform.eulerAngles.y);
		 	}

		 	if (Input.GetKeyDown(KeyCode.RightBracket)){ 
		 		mainCameraTransform.eulerAngles = Vector3(0,270,0);
		  		//Debug.Log(mainCameraTransform.eulerAngles.y);
		 	}

	 	}else if(mainCameraTransform.eulerAngles.y == 270 && mainCameraTransform.eulerAngles.y <= 360){ // Value -90 / 270

		 	if (Input.GetKeyDown(KeyCode.LeftBracket )){ // Negative 180
		 		mainCameraTransform.eulerAngles = Vector3(0,180,0);
		 		//Debug.Log(mainCameraTransform.eulerAngles.y);
		 	}

		 	if (Input.GetKeyDown(KeyCode.RightBracket )){ // Value 0
		 		mainCameraTransform.eulerAngles = Vector3(0,0,0);
		  		//Debug.Log(mainCameraTransform.eulerAngles.y);
		 	}
		}
 


///////////////////////


/* 	if (Input.GetKeyDown(KeyCode.LeftBracket )){
 		mainCameraTransform.eulerAngles.y = updatedCamera.y + -90;
 		//Debug.Log('Pressed -90');
 		Debug.Log(mainCameraTransform.eulerAngles.y);
 	}
 	if (Input.GetKeyDown(KeyCode.RightBracket )){
 		mainCameraTransform.eulerAngles.y = updatedCamera.y + 90;
 		//Debug.Log('Pressed +90');
  		Debug.Log(mainCameraTransform.eulerAngles.y);
 	}*/

}