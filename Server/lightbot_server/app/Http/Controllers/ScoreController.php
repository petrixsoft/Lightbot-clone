<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

use App\Http\Requests;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\DB;
use App\Level;
use App\Score;

class ScoreController extends Controller
{
    //
	
	public function ResetBD()
	{
		$levelData = Level::all();
		$scoreData = Score::all();

		foreach($levelData as $lData)
		{
			$lData->delete();
		}

		foreach($scoreData as $sData)
		{
			$sData->delete();
		}
	
		// Now we verify that everything  has been deleted

		$emptyLevelData = Level::all();
		$emptyScoreData = Score::all();

		if(count($emptyLevelData) == 0 && count($emptyScoreData) == 0)
		{
			for($i = 0 ; $i < 6 ; $i++)
			{
				$newLevel = new Level;
				$newLevel->name = 'L'.($i+1);

				$newLevel->save();
			}

			return ['error' => '', 'result' => 'Ok', 'data' => 'DB Cleared!'];
		}
		else
		{
			return ['error' => 'The DB has not been reset due to a problem', 'result' => 'Error', 'data' => ''];
		}
	}
	
	public function GetList(Request $request)
	{
		$levelName = $request->input('levelname');
		
		$dataList = Score::where(['levelname' => $levelName])->orderBy('score', 'desc')->take(10)->get();
		
		return ['error' => '', 'result' => 'Ok', 'data' => $dataList];
	}

	public function AddScore(Request $request)
	{
		$levelName = $request->input('levelname');
		$scoreNumber = $request->input('score');
		$userName = $request->input('username');

		$score = new Score;
		$score->username = $userName;
		$score->score = $scoreNumber;
		$score->levelname = $levelName;

		$scoreSaved = $score->save();

		if($scoreSaved == true)
		{
			return response()->json(['error' => '', 'result' => 'Ok', 'data' => '']);
		}
		else
			return response()->json(['error' => 'The score was not saved', 'result' => 'Error', 'data' => '']);
	}
}
