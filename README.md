# チクミン　ミニ
https://unityroom.com/games/tikumin
## デモ映像
[!['デモ映像'](https://github.com/tmsb0606/Chikumin/assets/120014601/232b17a6-a52e-4eb8-b40a-f88acc7c9a12)](https://www.youtube.com/watch?v=I2N4IMqfBU0)
## 概要
開発背景：東京都23区の特徴を組み込んだゲームを作ったら面白いと思ったため開発を始めた。<br>
開発期間：2ヶ月～<br>
メンバー：デザイナ1人、エンジニア1人<br>
開発環境：Unity <br>
開発言語：C# <br>
使用技術：UniTask、Linq、Addresables<br>
工夫点：<br>
・Terrainで配置した木のレイヤーをそれぞれ変えることが出来ないためTerrainの木の情報が格納された配列から木を生成するようにした。<br>
・Tipsの情報が入ったcsvファイルをサーバー(GithubPages)に置きそれをAddressableで読み込むようにし、Tipsの追加を楽にした。<br>
・講義で学んだデザインパターンをゲーム内に組み込んでみたかったため、ゲームの状況管理にステートパターンを使用した。

## ゲーム説明
### 内容
マップ内にあるアイテムを集めてお金を稼ぐゲーム。それぞれのキャラクターに特性があるのでそれを生かして立ち回ることでアイテムを集めやすくなる。
キャラクターのレベルを上げるとそれぞれの性能が上がる。運ぶアイテムには効果があるものがあり、拠点に持ち帰ると発動する。
### 操作方法
移動：WASD<br>
カメラ回転：R<br>
突撃：T<br>
チクミンを投げる：マウス左クリック<br>
チクミンを呼ぶ：マウス右クリック<br>
チクミンを待機させる：マウスホイールクリック<br>
レベルアップ：1~3キー

### キャラクター
・アダチクミン<br>
    攻撃力が高い。レベルを上げると攻撃力UP<br>
・チヨダクミン<br>
    ATMからお金を取り出すことができる。レベルを上げると取り出せるお金の量がUP<br>
・ミナトクミン<br>
    一度に大量のアイテムを運ぶことができる。レベルを上げると運べるアイテム量がUP<br>
### アイテム
・警備員<br>
    倒すとレベルアップ。拠点に運ぶとチクミンが1人増える。
・時計<br>
    拠点に運ぶと制限時間が10秒延長される。
