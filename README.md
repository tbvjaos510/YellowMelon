# YellowMelon (참외) 
<img src="./melon_color.svg" width="100px">

사용자가 아티스트가 되어 쉽게 음악을 공유하고 사용자들은 노래를 쉽게 들을 수 있는 UWP 음악 앱 입니다.

> Tip: 참외는 영어로 [KoreanMelon](https://ko.wikipedia.org/wiki/%EC%B0%B8%EC%99%B8)이라고 한다. _노란 멜론_
## Uses
* C# (UWP)
* SQLite

# 보고서 내용

## 1. 문제 정의 및 분석
기존 멜론과 비슷한 어플리케이션을 만들고 싶다. 대신 사용자 누구나 아티스트가 되어 노래를 올릴 수 있고 사용자들도 노래를 재생목록에 담아서 들을 수 있는 시스템을 개발할려고 한다.
## 2. 요구사항 분석 - 현행 업무 분석 및 요구기능 분석
사용자와 아티스트끼리 노래를 쉽게 공유할 수 있고 누구나 노래를 올릴 수 있다. 또한 노래를 youtube link를 통해 쉽게 올릴 수 있고 재생할 수 있다. 재생목록을 지원해 사용자마다 여러 재생목록으로 자신이 듣는 노래를 목록별로 나누어서 정리하고 싶다.
## 3. 설계과정
전체 설계 과정 간략 설명

ERD 설계 -> AQueryTool 설계 -> UWP Model 설계 -> Database 구축 (SQLite)

DB 설계

<img src="image/_03.png" width=400>

테이블 생성 및 관계 설정

<img src="image/_04.png" width=900>

## 4. 메뉴/화면 설계 - 폼, 쿼리 등
### 로그인

<img src="image/_01.png" width=600>

### 메인 화면

<img src="image/_02.png" width=600>

## 5. 사용설명서 – 사용자별 동작 설명


> ## 사용자 입장

<img src="image/_06.png" width=200 style="float:right">
<img src="image/_05.png" width=150 style="float:right">

### 재생목록 관리
- 재생목록 추가를 누르면 재생목록에 추가가 됨
- 재생목록 삭제를 하면 재생목록이 삭제가 됨
- 노래 목록에서 더블클릭 하면 재생 목록에 추가가 된다.
- 재생목록에 있는 노래를 더블클릭하면 재생이 된다.

<br>
<br>

### 노래 재생

<img src="image/_21.png">

- 노래가 재생중이지 않으면 재생 버튼을 누를 시 재생목록에 첫 번째 노래가 재생된다 (없으면 알림)
- 노래가 재생중이면 재생, 일시정지 된다.
- 다음 노래와 이전 노래를 클릭시 재생목록에서 다음, 이전 노래로 간다
- 소리를 조절할 수 있다.
- 노래의 위치를 Slidebar로 움직일 수 있다.

### 아티스트 정보
<img src="image/_08.png" width=40%>
<img src="image/_07.png" width=40%>

- 노래 목록에서 노래를 오른쪽 클릭한 뒤, 아티스트 보기를 누르면 아티스트의 정보를 볼 수 있다.
- 해당 아티스트의 노래와 정보를 볼 수 있다.

### 곡 좋아요
- 자신이 마음에 드는 곡에 좋아요를 표시할 수 있다.

### 아티스트로 변환
<img src="image/_10.png" width=28% style="float:right">
<img src="image/_09.png" width=70%>

- 아티스트로 전환 버튼을 누르면 사용자가 아티스트가 될 수 있다.

> ## 아티스트 입장
### 노래 등록
<img src="image/_11.png" width=28% style="float:right">
<img src="image/_22.png" width=70%>

- 상단에 노래 올리기 버튼을 누르면 자신의 노래를 추가할 수 있다.

**아티스트의 기존 기능은 일반 사용자와 같다**

## 6. 주요 코드 분석
<img src="image/_13.png" style="float:right" width=20%>

* UWP 앱으로 MVVM 패턴을 사용하였다. ->

Model의 private 필드는 데이터베이스의 컬럼 이름과 같게 하였다.

* User Model 정의

<img src="image/_12.png" width=40%>

이렇게 하면 좋은 점이 Dapper라는 라이브러리를 사용하여 쉽게 Select
할 수 있다.

* 사용 예 ↓ (Join을 사용하여 데이터를 얻는데 메소드 하나만 사용한다)

<img src="image/_14.png" width=100%>

Youtube 음악을 플레이 하기 위해 libvideo라는 라이브러리를 사용하였다.

(https://github.com/i3arnon/libvideo)

해당 라이브러리로 유튜브 링크를 통해 비디오 (내 프로젝트에서는 음악만 사용)를 불러와 윈도우에서 재생시켰다.

* 해당 라이브러리를 사용하는 코드이다
<img src="image/_15.png" >

이 프로젝트에서 리스트를 많이 사용하는데, 리스트는 전부 데이터 바인딩으로 추가, 삭제가 쉽고 UI 쪽에서도 간단하고 가독성 좋은 코드를 짜였다. 

* 아래는 음악 리스트를 바인딩 한 것이다

<img src="image/_16.png" >


* Main UI 구조는 아래와 같이 이루어져 있다.

<img src="image/_17.png" >

**(위에 MusicController.xaml의 위부분은 UserInfoController로 작게 있다. (노래 버튼까지) 
잘못 그렸다)**

MainPage.xaml에 보면 위 사진과 같이 나누어진 부분을 볼 수 있다.
<img src="image/_18.png" style="float:right">

* 데이터베이스 사용은 SQLite를 사용하였고 DataAccess 프로젝트를 새로 만들어 참조하였다. ->

DataAccess.cs 에서는 테이블과 초기 예시 데이터들을 넣는다. 전부 static으로 설계하여서 모든 프로젝트에서 DataAccess.db를 통해 connection에 접근이 가능하다.

* DataAccess.cs 구조

<img src="image/_19.png" >

SQLite 파일은 프로그램에서 자동으로 생성해 주어서 따로 데이터베이스 서버를 열 필요가 없다.

## 7. 프로젝트 후기(5줄 이상)
이번 프로젝트를 하면서 SQLite, UWP에 대한 지식이 엄청나게 많이 늘었고 이러한 프로그램을 만들 때 자신감이 생겼다. 또한 실제로 작동하고 음악을 들을 수 있는 프로그램을 만들어서 이 프로그램으로 노래를 들어보니 기분이 좋았다. 처음에 계획하고 프로그램을 제작할 때 보다 제작하면서 새로운 기술들을 보고 적용시키는 것들이 새로웠고 UWP(WPF)에 대한 개발 속도가 점점 더 빨라지는 것을 느끼게 되었다. 

(아래 사진을 보면 내가 개발중에 실제 윈도우에 미디어 플레이어에도 접근이 가능하다는 것을 알게 되었고 코드를 수정해 바로 적용하였다. 내 뮤직 플레이어와 윈도우 음악 컨트롤러가 공유가 된다.)
<img src="image/_20.png" >
아쉬운 점이 있다면 유튜브에서 노래를 다운받아서 재생하기 때문에 재생 시간이 지연이 된다.
